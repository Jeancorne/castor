import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from "jwt-decode";
import { Alerts } from '../../alerts/alerts';
@Injectable({
    providedIn: 'root'
})
export class LocalStorageService {
    constructor(private cookie: CookieService) { }


    setToken(token: string) {
        this.cookie.set('token', token, 1, "/");
    }

    clear() {
        this.cookie.delete("token", "/");
        localStorage.clear();
    }

    decodeToken(token: string): any {
        try {
            return jwtDecode(token);
        } catch (error: any) {
            Alerts.warning("Error", error, "Aceptar");
        }
    }

}