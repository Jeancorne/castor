import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/loginPage/login.component';
import { NotFoundComponent } from './shared/notFound/notFound.component';
import { CommonModule, registerLocaleData } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzResultModule } from 'ng-zorro-antd/result';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { IconDefinition, IconModule } from '@ant-design/icons-angular';
import { NZ_ICONS, NzIconModule } from 'ng-zorro-antd/icon';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import * as AllIcons from '@ant-design/icons-angular/icons';
import { MainComponent } from './pages/main/main.component';
const antDesignIcons: Record<string, IconDefinition> = AllIcons;
const icons: IconDefinition[] = Object.keys(antDesignIcons).map(key => antDesignIcons[key])
import spanish from '@angular/common/locales/es';
import { es_ES, NZ_I18N } from 'ng-zorro-antd/i18n';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { demoInterceptor } from './shared/interceptors/authInterceptorService';
registerLocaleData(spanish);

@NgModule({
  declarations: [
    AppComponent
    , LoginComponent
    , MainComponent
    , NotFoundComponent

  ],
  imports: [
    BrowserModule
    , NzLayoutModule
    , BrowserAnimationsModule
    , AppRoutingModule
    , CommonModule
    , ReactiveFormsModule
    , NzTypographyModule
    , NzFormModule
    , NzButtonModule
    , NzInputModule
    , NzGridModule
    , IconModule
    , FormsModule
    , NzMenuModule
    , NzSpinModule
    , NzSelectModule
    , NzDropDownModule
    , NzResultModule
    , NzIconModule.forChild(icons)
    , NzTabsModule
  ],
  providers: [
    provideHttpClient(),
    { provide: NZ_ICONS, useValue: icons },
    {
      provide: NZ_I18N,
      useFactory: () => {
        return es_ES;
      },
      deps: [LOCALE_ID]
    },
    provideHttpClient(withInterceptors([demoInterceptor])),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
