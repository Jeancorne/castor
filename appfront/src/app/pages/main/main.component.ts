import { Component } from "@angular/core";
import jsonMenu from './menu.json';
import { Router } from "@angular/router";
import { LocalStorageService } from "../../shared/services/localStorage/localStorageService";

@Component({
    selector: 'main-component',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.css']
})
export class MainComponent {
    isCollapsed = false;
    lsMenus: any[] = [];
    constructor(private router: Router,
        private localServices: LocalStorageService) {
        this.lsMenus = jsonMenu;
    }

    async navigation(url: string) {
        this.router.navigate([url])
    }

    async logout() {
        this.localServices.clear();
        this.router.navigate(['login']);
    }
}
