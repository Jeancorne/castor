import { HttpContextToken, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from "ngx-cookie-service";
import { JwtHelperService } from "@auth0/angular-jwt";
import { LocalStorageService } from '../services/localStorage/localStorageService';
import { Alerts } from '../alerts/alerts';
import { Router } from '@angular/router';
import { of } from 'rxjs';

export const BYPASS_LOG = new HttpContextToken(() => false);

export const demoInterceptor: HttpInterceptorFn = (req, next) => {
    const cookieService = inject(CookieService);
    const localStorageService = inject(LocalStorageService);
    const router = inject(Router);
    const isLoggedIn = cookieService.check('token');
    if (isLoggedIn == false) {
        if (req.context.get(BYPASS_LOG) == true) return next(req)
    } else {
        const jwtHelper: JwtHelperService = new JwtHelperService();
        if (jwtHelper.isTokenExpired(cookieService.get('token'))) {
            localStorageService.clear();
            router.navigate(['login']);
            Alerts.warning("Error", "La sesión se venció", "Aceptar");
            setTimeout(() => {
                window.location.reload();
            }, 2000);
            return of();
        }
        const token = cookieService.get("token");
        req = req.clone({
            setHeaders: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`
            }
        });
    }
    return next(req);
};