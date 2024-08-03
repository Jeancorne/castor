import { Injectable } from '@angular/core';
import { HttpClient, HttpContext } from '@angular/common/http';
import { AddUserDto } from '../../../../pages/login/models/addUserDto';
import { Alerts } from '../../../alerts/alerts';
import { loginModelDto } from '../../../../pages/login/models/loginModelDto';
import { environment } from '../../../../../environments/environment';
import { BYPASS_LOG } from '../../../interceptors/authInterceptorService';

@Injectable({
    providedIn: 'root'
})
export class UserServices {

    private url = '';
    constructor(private http: HttpClient) {
        this.url = environment.urlBack;
    }

    loginSystem(data: loginModelDto) {
        return new Promise<any>((resolve, reject) => {
            this.http.post<any>(`${this.url}api/v1/user/loginSystem`, data,
                {
                    context: new HttpContext().set(BYPASS_LOG, true),
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


    registerUser(data: AddUserDto) {
        return new Promise<any>((resolve, reject) => {
            this.http.post<any>(`${this.url}api/v1/user/registerUser`, data, { context: new HttpContext().set(BYPASS_LOG, true) }).subscribe({
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