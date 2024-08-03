import { NgModule } from "@angular/core";
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { CommonModule } from "@angular/common";
import { BreadcrumbComponent } from "./breadcrumb.component";

@NgModule({
    imports: [
        NzBreadCrumbModule
        , CommonModule
    ],
    declarations: [
        BreadcrumbComponent,
    ],
    exports:
    [
        BreadcrumbComponent
    ]

})
export class BreadcrumbModule {
}