import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/loginPage/login.component';
import { AuthGuardLogin } from './shared/guards/guardLogin';
import { NotFoundComponent } from './shared/notFound/notFound.component';
import { MainComponent } from './pages/main/main.component';
import { AuthGuard } from './shared/guards/authGuard';

// import { AuthGuardLogin } from './shared/guards/guardLogin';
// import { MainComponent } from './pages/main/main.component';
// import { AuthGuard } from './shared/guards/authGuard';
// import { NotFoundComponent } from './shared/notFound/notFound.component';
// import { MainComponent } from './pages/main/main.component';
// import { LoginComponent } from './pages/login/loginPage/login.component';
// import { AuthGuardLogin } from './shared/guards/guardLogin';
// import { AuthGuard } from './shared/guards/authGuard';
// import { NotFoundComponent } from './shared/notFound/notFound.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [AuthGuardLogin],
    children: [
      {
        path: '**',
        component: NotFoundComponent
      }
    ]
  },
  {
    path: 'main',
    component: MainComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        loadChildren: () => import(`./modules/employees/employees.module`).then(m => m.EmployeesModule),
      },
      { path: '**', component: NotFoundComponent },
    ]
  },
  { path: '**', component: NotFoundComponent },
]

@NgModule({
  declarations: [

  ],
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }