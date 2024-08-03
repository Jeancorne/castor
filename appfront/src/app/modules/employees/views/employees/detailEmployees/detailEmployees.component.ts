import { Component, Input, OnInit, signal } from "@angular/core";
import { DetailEmployeeDto } from "../../../models/employeesDto/detailEmployeeDto";
import { FormGroup, UntypedFormControl, Validators } from "@angular/forms";
import { NzDrawerRef } from "ng-zorro-antd/drawer";
import { Alerts } from "../../../../../shared/alerts/alerts";
import { forceValidation } from "../../../../../shared/utils/validationForms";
import { JobsTitleDto } from "../../../models/jobsDto/jobsTitleDto";
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { Observable, Observer } from 'rxjs';
import { EmployeeServices } from "../../../../../shared/services/apiBack/Employee/employeeServices";

@Component({
    selector: 'details-employee-component',
    templateUrl: './detailEmployees.component.html',
    styleUrls: ['./detailEmployees.component.css']
})

export class DetailEmployeesComponent implements OnInit {
    @Input() isNew = true;
    @Input() id: number | null = null;
    public loading = signal(true);
    public detailsEmployee = new DetailEmployeeDto();
    public lsJobsTitles: JobsTitleDto[] = [];
    public formDetailsEmployee = new FormGroup({
        name: new UntypedFormControl(null, [Validators.required]),
        identification: new UntypedFormControl(null, [Validators.required]),
        dateRegistry: new UntypedFormControl(null, [Validators.required]),
        idJobTitle: new UntypedFormControl(null, [Validators.required]),
    })

    constructor(private drawerRef: NzDrawerRef, private employeeServices: EmployeeServices) {

    }

    async ngOnInit() {
        try {
            this.loading.set(true);
            await this.getListJobsTitles();
            await this.getDetailEmployee();
            this.loading.set(false);
        } catch (error: any) {
            this.loading.set(false);
            Alerts.warning("Error", error, "Aceptar");
        }
    }

    setDataModel() {
        this.formDetailsEmployee.controls.name.setValue(this.detailsEmployee.name);
        this.formDetailsEmployee.controls.identification.setValue(this.detailsEmployee.identification);
        this.formDetailsEmployee.controls.dateRegistry.setValue(this.detailsEmployee.dateRegistry);
        this.formDetailsEmployee.controls.idJobTitle.setValue(this.detailsEmployee.idJobTitle);
    }

    getDataModel() {
        this.detailsEmployee.name = this.formDetailsEmployee.controls.name.value;
        this.detailsEmployee.identification = this.formDetailsEmployee.controls.identification.value;
        this.detailsEmployee.dateRegistry = this.formDetailsEmployee.controls.dateRegistry.value;
        this.detailsEmployee.idJobTitle = this.formDetailsEmployee.controls.idJobTitle.value;
    }

    async getListJobsTitles() {
        try {
            this.lsJobsTitles = await this.employeeServices.getAllListJobs().then((data: any) => { return data; }).catch((err) => { throw err; });
        } catch (error: any) {
            this.loading.set(false);
            Alerts.warning("Error", error, "Aceptar");
        }
    }

    async getDetailEmployee() {
        try {
            if (this.isNew == false) {
                this.detailsEmployee = await this.employeeServices.getDetailEmployeeById(this.id!).then((data: any) => { return data; }).catch((err) => { throw err; });
                this.setDataModel();
            }
        } catch (error: any) {
            this.loading.set(false);
            Alerts.warning("Error", error, "Aceptar");
        }
    }

    async btnUpdate() {
        try {
            forceValidation(this.formDetailsEmployee);
            if (this.formDetailsEmployee.valid) {
                this.loading.set(true);
                this.getDataModel();
                let result;
                if (this.isNew == true) {
                    result = await this.employeeServices.registerEmployee(this.detailsEmployee);
                } else {
                    result = await this.employeeServices.updateEmployee(this.detailsEmployee);
                }
                if (result != null) {
                    this.drawerRef.close(true);
                }
                this.loading.set(false);
            }
        } catch (error: any) {
            this.loading.set(false);
            Alerts.warning("Error", error, "Aceptar");
        }
    }

    beforeUpload = (file: NzUploadFile): Observable<boolean> => {
        return new Observable((observer: Observer<boolean>) => {
            if (this.isImage(file)) {
                this.convertFileToBase64(file).subscribe({
                    next: (base64String: string) => {
                        const base64Regex = /^data:image\/(?:png|jpeg|jpg);base64,/;
                        this.detailsEmployee.base64 = base64String.replace(base64Regex, '');
                        this.detailsEmployee.fileName = file.name;
                        observer.next(true);
                        observer.complete();
                    },
                    error: (error) => {
                        Alerts.warning("Error", error, "Aceptar");
                        observer.next(false);
                        observer.complete();
                    }
                });
            } else {
                Alerts.warning("Error", "La imagen debe ser png/jpg", "Aceptar");
                observer.next(false);
                observer.complete();
            }
        });
    };

    convertFileToBase64(file: NzUploadFile): Observable<string> {
        return new Observable((observer: Observer<string>) => {
            const reader = new FileReader();
            reader.readAsDataURL(file as unknown as File);
            reader.onload = () => {
                observer.next(reader.result as string);
                observer.complete();
            };
            reader.onerror = (error) => {
                observer.error(error);
            };
        });
    }

    isImage(file: NzUploadFile): boolean {
        return (file?.type ?? '').startsWith('image/');
    }
}