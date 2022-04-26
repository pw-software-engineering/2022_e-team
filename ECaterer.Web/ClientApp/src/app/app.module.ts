import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { GridModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DateInputsModule } from "@progress/kendo-angular-dateinputs";
import { LabelModule } from "@progress/kendo-angular-label";
import { InputsModule } from "@progress/kendo-angular-inputs";
import { LayoutModule } from "@progress/kendo-angular-layout";
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { IndicatorsModule } from "@progress/kendo-angular-indicators";
import { CookieModule } from "ngx-cookie";
import { NavigationModule } from "@progress/kendo-angular-navigation";

/* Components */
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ClientRegistration } from './registration/client-registration/client-registration.component';
import { ClientLogin } from './registration/client-login/client-login.component';
import { WorkerLogin } from './registration/worker-login/worker-login.component';
import { AuthGuard } from './authGuard/authGuard';
import { LoginGuard } from './authGuard/loginGuard';
import { Diets } from './diets/diets.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ClientRegistration,
    ClientLogin,
    WorkerLogin,
	Diets
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'diets', pathMatch: 'full'/* component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard]*/ },
      { path: 'home', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'client/register', component: ClientRegistration, pathMatch: 'full', canActivate: [LoginGuard] },
      { path: 'client/login', component: ClientLogin, pathMatch: 'full', canActivate: [LoginGuard] },
      { path: 'worker/login', component: WorkerLogin, pathMatch: 'full' },
      { path: 'diets', component: Diets, pathMatch: 'full', canActivate: [AuthGuard] }
    ]),
    GridModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    DateInputsModule,
    InputsModule,
    LayoutModule,
    LabelModule,
    ButtonsModule,
    CookieModule.forRoot(),
    NavigationModule,
    IndicatorsModule
  ],
  providers: [Title],
  bootstrap: [AppComponent]
})
export class AppModule { }
