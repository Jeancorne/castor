import { Component, Input } from "@angular/core";
import { breadCrumsDto } from "../models/breadcrums/breadCrumsDto";


@Component({
    selector: 'breadcrumb-component',
    templateUrl: './breadcrumb.component.html',
})
export class BreadcrumbComponent {
    @Input() inplsbreadcrumb: breadCrumsDto[] = [];
}