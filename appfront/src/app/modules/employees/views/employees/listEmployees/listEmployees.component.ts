import { Component, OnInit, signal } from "@angular/core";
import { environment } from "../../../../../../environments/environment";
import { FilterListEmployeesDto } from "../../../models/employeesDto/filterListEmployeesDto";
import { Alerts } from "../../../../../shared/alerts/alerts";
import { ListEmployeesDto } from "../../../models/employeesDto/listEmployeesDto";
import { NzTableQueryParams } from "ng-zorro-antd/table";
import { ordersColumns } from "../../../../../shared/utils/ordersTable";
import { NzDrawerRef, NzDrawerService } from "ng-zorro-antd/drawer";
import { DetailEmployeesComponent } from "../detailEmployees/detailEmployees.component";
import { EmployeeServices } from "../../../../../shared/services/apiBack/Employee/employeeServices";
import { MetaDataDto } from "../../../../../shared/models/metaData/metaDataDto";

@Component({
    selector: 'list-employees-component',
    templateUrl: './listEmployees.component.html'
})
export class ListEmployeesComponent implements OnInit {
    public breadcrumb: any = [{ name: "Inicio" }, { name: "Menu" }, { name: "Empleados" }];
    public loading = signal(true);
    public pageSize = environment.pageSize;
    public filter = new FilterListEmployeesDto();
    public lsEmployees: ListEmployeesDto[] = [];
    public metaData = new MetaDataDto<ListEmployeesDto>();

    constructor(private drawerService: NzDrawerService, private employeeServices: EmployeeServices) {
        this.filter.PageNumber = 1;
        this.filter.PageSize = this.pageSize;
    }

    async ngOnInit() {
        try {
            this.loading.set(true);
            await this.getListEmployees();
            this.loading.set(false);
        } catch (error: any) {
            this.loading.set(false);
            Alerts.warning("Error", error, "Aceptar");
        }
    }

    async btnFilter(clear: boolean) {
        if (clear) {
            this.filter.identification = null
            this.filter.name = null
        }
        await this.getListEmployees();
    }

    async getListEmployees() {
        try {
            this.loading.set(true);
            this.metaData = await this.employeeServices.getListEmployees(this.filter).then((data: any) => { return data; }).catch((err) => { throw err; });;
            this.lsEmployees = this.metaData.data
            this.loading.set(false);
        } catch (error: any) {
            this.loading.set(false);
            Alerts.warning("Error", error, "Aceptar");
        }
    }

    changePage(num: any) {
        this.filter.PageNumber = num;
        this.getListEmployees();
    }

    changeSize(num: any) {
        this.filter.PageSize = num;
        this.filter.PageNumber = 1;
        this.getListEmployees();
    }

    onQueryParamsChange(params: NzTableQueryParams): void {
        ordersColumns(params, this.lsEmployees);
    }

    async getDetailsEmployee(id: number | null, isNew = true) {
        try {
            const drawerRef: NzDrawerRef = this.drawerService.create({
                nzTitle: (isNew == true ? "Crear empleado" : "Actualizar empleado"), nzContent: DetailEmployeesComponent, nzWidth: '850px',
                nzContentParams: {
                    isNew: isNew,
                    id: id,
                }
            });
            drawerRef.afterClose.subscribe((data: any) => {
                if (data) {
                    this.getListEmployees();
                }
            });
        } catch (error: any) {
            this.loading.set(false);
            Alerts.warning("Error", error, "Aceptar");
        }
    }
}