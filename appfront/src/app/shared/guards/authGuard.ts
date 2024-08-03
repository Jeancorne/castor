import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard {
    constructor(
        private cookie: CookieService,
        private router: Router) { }
    canActivate() {
        const isLoggedIn = this.cookie.check('token');
        if (isLoggedIn == true) {
            return true;
        } else {
            this.router.navigate(['/login']);
            return false;
        }
    }
}