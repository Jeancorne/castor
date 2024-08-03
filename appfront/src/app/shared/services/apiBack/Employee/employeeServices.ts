import { Injectable } from '@angular/core';
import { HttpClient, HttpContext } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { Alerts } from '../../../alerts/alerts';
import { JobsTitleDto } from '../../../../modules/employees/models/jobsDto/jobsTitleDto';
import { DetailEmployeeDto } from '../../../../modules/employees/models/employeesDto/detailEmployeeDto';
import { FilterListEmployeesDto } from '../../../../modules/employees/models/employeesDto/filterListEmployeesDto';
import { ListEmployeesDto } from '../../../../modules/employees/models/employeesDto/listEmployeesDto';
import { MetaDataDto } from '../../../models/metaData/metaDataDto';
import { BYPASS_LOG } from '../../../interceptors/authInterceptorService';

@Injectable({
    providedIn: 'root'
})
export class EmployeeServices {

    private url = '';
    constructor(private http: HttpClient) {
        this.url = environment.urlBack;
    }

    registerEmployee(data: DetailEmployeeDto) {
        return new Promise<DetailEmployeeDto>((resolve, reject) => {
            this.http.post<DetailEmployeeDto>(`${this.url}api/v1/employees/registerEmployee`, data, {
                context: new HttpContext().set(BYPASS_LOG, false),
            }).subscribe({
                next: (data: any) => {
                    resolve(data?.data);
                },
                error: (ex: any) => {
                    const result = Alerts.GetErrors(ex);
                    reject(result);
                }
            });
        });
    }

    updateEmployee(data: DetailEmployeeDto) {
        return new Promise<DetailEmployeeDto>((resolve, reject) => {
            this.http.put<DetailEmployeeDto>(`${this.url}api/v1/employees/updateEmployee`, data,
                {
                    context: new HttpContext().set(BYPASS_LOG, false),
                }
            ).subscribe({
                next: (data: any) => {
                    resolve(data?.data);
                },
                error: (ex: any) => {
                    const result = Alerts.GetErrors(ex);
                    reject(result);
                }
            });
        });
    }

    getAllListJobs() {
        return new Promise<JobsTitleDto[]>((resolve, reject) => {
            this.http.get<JobsTitleDto[]>(`${this.url}api/v1/employees/getAllListJobs`,
                {
                    context: new HttpContext().set(BYPASS_LOG, false),
                }
            ).subscribe({
                next: (data: any) => {
                    resolve(data?.data);
                },
                error: (ex: any) => {
                    const result = Alerts.GetErrors(ex);
                    reject(result);
                }
            });
        });
    }

    getListEmployees(filter: FilterListEmployeesDto) {
        return new Promise<MetaDataDto<ListEmployeesDto>[]>((resolve, reject) => {
            this.http.post<MetaDataDto<ListEmployeesDto>[]>(`${this.url}api/v1/employees/getListEmployees`, filter,
                {
                    context: new HttpContext().set(BYPASS_LOG, false),
                }
            ).subscribe({
                next: (data: any) => {
                    resolve(data?.data);
                },
                error: (ex: any) => {
                    const result = Alerts.GetErrors(ex);
                    reject(result);
                }
            });
        });
    }

    getDetailEmployeeById(id: number) {
        return new Promise<DetailEmployeeDto>((resolve, reject) => {
            this.http.get<DetailEmployeeDto>(`${this.url}api/v1/employees/getDetailEmployeeById/${id}`,
                {
                    context: new HttpContext().set(BYPASS_LOG, false),
                }
            ).subscribe({
                next: (data: any) => {
                    resolve(data?.data);
                },
                error: (ex: any) => {
                    const result = Alerts.GetErrors(ex);
                    reject(result);
                }
            });
        });
    }

}