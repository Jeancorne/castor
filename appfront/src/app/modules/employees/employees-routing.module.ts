import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { ListEmployeesComponent } from "./views/employees/listEmployees/listEmployees.component";
import { NotFoundComponent } from "../../shared/notFound/notFound.component";


export const routes: Routes = [
    {
        path: 'list-employees',
        component: ListEmployeesComponent
    },    
    {
        path: '*',
        component: NotFoundComponent
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class EmployeesRoutingModule { }