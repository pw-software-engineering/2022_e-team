import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { ClientRegistration } from './registration/client-registration/client-registration.component';
import { ClientLogin } from './registration/client-login/client-login.component';
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
import { DelivererHistoryComponent } from './order/delivererHistory/delivererHistory.component';
import { ProducerLogin } from './registration/producer-login/producer-login.component';
import { DelivererLogin } from './registration/deliverer-login/deliverer-login.component';
import { AuthInterceptor } from './registration/api/authInterceptor.service';

/* Guards */
import { ClientGuard } from './authGuard/clientGuard';
import { ClientLoginGuard } from './authGuard/clientLoginGuard';
import { DelivererGuard } from './authGuard/delivererGuard';
import { DelivererLoginGuard } from './authGuard/delivererLoginGuard';
import { ProducerGuard } from './authGuard/producerGuard';
import { ProducerLoginGuard } from './authGuard/producerLoginGuard';

@NgModule({
  declarations: [
    AppComponent,
    ClientRegistration,
    ClientLogin,
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
    ClientOrdersComponent,
    DelivererHistoryComponent,
    ProducerLogin,
    DelivererLogin
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'client/diets', pathMatch: 'full', canActivate: [ClientGuard] },
      { path: 'client/register', component: ClientRegistration, pathMatch: 'full', canActivate: [ClientLoginGuard] },
      { path: 'client/login', component: ClientLogin, pathMatch: 'full', canActivate: [ClientLoginGuard] },
      { path: 'producer/login', component: ProducerLogin, pathMatch: 'full', canActivate: [ProducerLoginGuard] },
      { path: 'deliverer/login', component: DelivererLogin, pathMatch: 'full', canActivate: [DelivererLoginGuard] },
      { path: 'client/diets', component: DietsComponent, pathMatch: 'full', canActivate: [ClientGuard] },
      { path: 'client/diets/:id', component: MealsComponent, pathMatch: 'full', canActivate: [ClientGuard] },
      { path: 'producer/diets', component: ProducerDietsComponent, pathMatch: 'full', canActivate: [ProducerGuard] },
      { path: 'producer/diets/:id', component: EditDietComponent, pathMatch: 'full', canActivate: [ProducerGuard] },
      { path: 'client/cart', component: CartComponent, pathMatch: 'full', canActivate: [ClientGuard] },
      { path: 'deliverer/orders', component: DelivererOrdersComponent, pathMatch: 'full', canActivate: [DelivererGuard] },
      { path: 'producer/orders', component: ProducerOrdersComponent, pathMatch: 'full', canActivate: [ProducerGuard] },
      { path: 'producer/orders/:id', component: PreviewOrderComponent, pathMatch: 'full', canActivate: [ProducerGuard] },
      { path: 'producer/orders/:id/complaint', component: ProducerComplaintComponent, pathMatch: 'full', canActivate: [ProducerGuard] },
      { path: 'client/orders', component: ClientOrdersComponent, pathMatch: 'full', canActivate: [ClientGuard] },
      { path: 'deliverer/history', component: DelivererHistoryComponent, pathMatch: 'full', canActivate: [DelivererGuard] },
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
  providers: [
    Title,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
