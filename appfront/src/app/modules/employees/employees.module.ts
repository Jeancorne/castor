import { LOCALE_ID, NgModule } from "@angular/core";
import { BreadcrumbModule } from "../../shared/breadcrumb/breadcrumb.module";
import { EmployeesRoutingModule } from "./employees-routing.module";
import { CommonModule, DatePipe } from "@angular/common";
import { NzSpinModule } from "ng-zorro-antd/spin";
import { NzInputModule } from "ng-zorro-antd/input";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { ListEmployeesComponent } from "./views/employees/listEmployees/listEmployees.component";
import { NzTableModule } from 'ng-zorro-antd/table';
import { DetailEmployeesComponent } from "./views/employees/detailEmployees/detailEmployees.component";
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { demoInterceptor } from "../../shared/interceptors/authInterceptorService";

@NgModule({
    imports: [
        EmployeesRoutingModule
        , BreadcrumbModule
        , CommonModule
        , NzSpinModule
        , NzInputModule
        , NzFormModule
        , NzButtonModule
        , FormsModule
        , ReactiveFormsModule
        , NzLayoutModule
        , NzTableModule
        , NzSelectModule
        , NzDrawerModule
        , NzDatePickerModule
        , NzUploadModule
        , NzPaginationModule

    ],
    declarations: [
        ListEmployeesComponent
        , DetailEmployeesComponent
    ],
    exports: [

    ],
    providers: [
        DatePipe,
        { provide: LOCALE_ID, useValue: "es_ES" },
        provideHttpClient(withInterceptors([demoInterceptor])),
    ],
})
export class EmployeesModule {
}