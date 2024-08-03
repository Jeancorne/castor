import { Component, signal } from "@angular/core";
import { Router } from "@angular/router";
import { loginModelDto } from "../models/loginModelDto";
import { AbstractControl, FormGroup, UntypedFormControl, ValidationErrors, Validators } from "@angular/forms";
import { LocalStorageService } from "../../../shared/services/localStorage/localStorageService";
import { Alerts } from "../../../shared/alerts/alerts";
import { forceValidation } from "../../../shared/utils/validationForms";
import { UserServices } from "../../../shared/services/apiBack/User/userServices";
import { AddUserDto } from "../models/addUserDto";
import { PattersValidation } from "../../../shared/utils/patterValidations";

@Component({
    selector: 'login-component',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']

})
export class LoginComponent {
    loading = signal(false);
    formLogin = new FormGroup({
        email: new UntypedFormControl(null, [Validators.required, Validators.pattern(PattersValidation.emailValidation)]),
        password: new UntypedFormControl(null, Validators.required),
    })
    addUserDto = new AddUserDto();
    formCreateUser = new FormGroup({
        name: new UntypedFormControl(null, Validators.required),
        lastName: new UntypedFormControl(null, Validators.required),
        email: new UntypedFormControl(null, [Validators.required, Validators.pattern(PattersValidation.emailValidation)]),
        password: new UntypedFormControl(null, Validators.required),
        repeatPassword: new UntypedFormControl(null, Validators.required),
    }, { validators: this.passwordMatchValidator })
    showPassword = false;
    loginModel: loginModelDto = new loginModelDto();

    async getDataModel() {
        this.loginModel.email = this.formLogin.value.email;
        this.loginModel.password = this.formLogin.value.password;
    }

    constructor(
        private router: Router,
        private userServices: UserServices,
        private localServices: LocalStorageService,
    ) { }

    async fnShowPassword() {
        this.showPassword = !this.showPassword;
    }

    async btnInit() {
        try {
            forceValidation(this.formLogin);
            if (this.formLogin.valid) {
                this.loading.set(true);
                this.getDataModel();
                const result = await this.userServices.loginSystem(this.loginModel);
                this.localServices.setToken(result);
                this.router.navigate(['/main']);
                this.loading.set(false);
            }
        } catch (error: any) {
            this.loading.set(false);
            Alerts.warning("Error", error, "Aceptar");
        }
    }

    async btnCreateUser() {
        try {
            forceValidation(this.formCreateUser);
            if (this.formCreateUser.valid) {
                this.loading.set(true);
                this.getDataModelCreate();
                const result = await this.userServices.registerUser(this.addUserDto);
                if (result) {
                    this.formCreateUser.reset();
                    Alerts.success("Registrado correctamente", '', "Aceptar");
                }
                this.router.navigate(['/main']);
                this.loading.set(false);
            }
        } catch (error: any) {
            this.loading.set(false);
            Alerts.warning("Error", error, "Aceptar");
        }
    }

    btnCancel() {
        this.formCreateUser.reset();
    }

    getDataModelCreate() {
        this.addUserDto.name = this.formCreateUser.controls.name.value;
        this.addUserDto.lastName = this.formCreateUser.controls.lastName.value;
        this.addUserDto.email = this.formCreateUser.controls.email.value;
        this.addUserDto.password = this.formCreateUser.controls.password.value;
    }


    passwordMatchValidator(formGroup: AbstractControl): ValidationErrors | null {
        const password = formGroup.get('password');
        const repeatPassword = formGroup.get('repeatPassword');
        return password && repeatPassword && password.value === repeatPassword.value ? null : { passwordsNotMatch: true };
    }

}