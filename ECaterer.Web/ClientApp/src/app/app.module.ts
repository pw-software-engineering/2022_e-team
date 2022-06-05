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
import { DialogsModule } from "@progress/kendo-angular-dialog";

/* Components */
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ClientRegistration } from './registration/client-registration/client-registration.component';
import { ClientLogin } from './registration/client-login/client-login.component';
import { WorkerLogin } from './registration/worker-login/worker-login.component';
import { AuthGuard } from './authGuard/authGuard';
import { LoginGuard } from './authGuard/loginGuard';
import { DietsComponent } from './diets/client/diets.component';
import { MealsComponent } from './meals/meals.component';
import { Navigation } from './navigation/navigation.component';
import { ProducerDietsComponent } from './diets/producer/producerDiets.component';
import { EditDietComponent } from './diets/editDiet/editDiet.component';
import { CartComponent } from './cart/cart.component';
import { DelivererOrdersComponent } from './order/deliverer/delivererOrders.component';
import { ProducerOrdersComponent } from './order/producer/producerOrders.component';
import { PreviewOrderComponent } from './order/previewOrder/previewOrder.component';
import { ProducerComplaintComponent } from './order/producerComplaint/producerComplaint.component';
import { ClientOrdersComponent } from './order/client/clientOrders.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ClientRegistration,
    ClientLogin,
    WorkerLogin,
    Navigation,
    DietsComponent,
    MealsComponent,
    ProducerDietsComponent,
    EditDietComponent,
    CartComponent,
    DelivererOrdersComponent,
    ProducerOrdersComponent,
    PreviewOrderComponent,
    ProducerComplaintComponent,
    ClientOrdersComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'client/diets', pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'home', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'client/register', component: ClientRegistration, pathMatch: 'full', canActivate: [LoginGuard] },
      { path: 'client/login', component: ClientLogin, pathMatch: 'full', canActivate: [LoginGuard] },
      { path: 'worker/login', component: WorkerLogin, pathMatch: 'full' },
      { path: 'client/diets', component: DietsComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'client/diets/:id', component: MealsComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'producer/diets', component: ProducerDietsComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'producer/diets/:id', component: EditDietComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'client/cart', component: CartComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'deliverer/orders', component: DelivererOrdersComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'producer/orders', component: ProducerOrdersComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'producer/orders/:id', component: PreviewOrderComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'producer/orders/:id/complaint', component: ProducerComplaintComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'client/orders', component: ClientOrdersComponent, pathMatch: 'full', canActivate: [AuthGuard] }
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
    IndicatorsModule,
    DialogsModule
  ],
  providers: [Title],
  bootstrap: [AppComponent]
})
export class AppModule { }
