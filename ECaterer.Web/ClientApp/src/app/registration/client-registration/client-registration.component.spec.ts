import { async, ComponentFixture, TestBed } from '@angular/core/testing';

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

/* Components */
import { HomeComponent } from '../../home/home.component';
import { ClientRegistration, IRegistrationData } from '../client-registration/client-registration.component';
import { ClientLogin } from '../client-login/client-login.component';

describe('ClientRegistration', () => {
  let component: ClientRegistration;
  let fixture: ComponentFixture<ClientRegistration>;

  let validRegistrationInputs: IRegistrationData[] = [
    {
      name: "Jan",
      surname: "Kowalski",
      email: "jan.kowalski@gmail.com",
      password: "p@ssw0rD",
      phone: "+48-123-123-123"
    }
  ];

  let invalidRegistrationInputs: IRegistrationData[] = [
    // invalid on name
    {
      name: "jan1",
      surname: "Kowalski",
      email: "jan.kowalski@gmail.com",
      password: "p@ssw0rD",
      phone: "+48-123-123-123"
    },
    // invalid on surname
    {
      name: "Jan",
      surname: "K",
      email: "jan.kowalski@gmail.com",
      password: "p@ssw0rD",
      phone: "+48-123-123-123"
    },
    // invalid on email
    {
      name: "Jan",
      surname: "Kowalski",
      email: "jan.kowalskigmail.com",
      password: "p@ssw0rD",
      phone: "+48-123-123-123"
    },
    // invalid on password : no special char
    {
      name: "Jan",
      surname: "Kowalski",
      email: "jan.kowalskigmail.com",
      password: "passw0rD",
      phone: "+48-123-123-123"
    },
    // invalid on password : no capital char
    {
      name: "Jan",
      surname: "Kowalski",
      email: "jan.kowalskigmail.com",
      password: "p@ssw0rd",
      phone: "+48-123-123-123"
    },
    // invalid on password : no number
    {
      name: "Jan",
      surname: "Kowalski",
      email: "jan.kowalskigmail.com",
      password: "p@ssworD",
      phone: "+48-123-123-123"
    },
    // invalid on password : no small char
    {
      name: "Jan",
      surname: "Kowalski",
      email: "jan.kowalskigmail.com",
      password: "P@SSW0RD",
      phone: "+48-123-123-123"
    },
    // invalid on phone
    {
      name: "Jan",
      surname: "Kowalski",
      email: "jan.kowalskigmail.com",
      password: "p@ssw0rD",
      phone: "+48-123-123-12A"
    },
  ];

  beforeAll(function () {
    jasmine.DEFAULT_TIMEOUT_INTERVAL = 999999;
  });

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        HomeComponent,
        ClientRegistration,
        ClientLogin
      ],
      imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
          { path: '', component: HomeComponent, pathMatch: 'full' },
          { path: 'client/register', component: ClientRegistration, pathMatch: 'full' },
          { path: 'client/login', component: ClientLogin, pathMatch: 'full' }
        ]),
        GridModule,
        BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        DateInputsModule,
        InputsModule,
        LayoutModule,
        LabelModule,
        ButtonsModule
      ],
      providers: [Title],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientRegistration);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should mark invalid on empty form', () => {
    component.form.markAllAsTouched();
    expect(component.form.valid).toBeFalsy();
  });

  it('should mark valid', () => {
    component.registrationData = validRegistrationInputs[0];
    component.form.markAllAsTouched();
    expect(component.form.valid).toBeTruthy();
  });

  it('should mark invalid on name', () => {
    component.registrationData = invalidRegistrationInputs[0];
    component.form.markAllAsTouched();
    expect(component.form.valid).toBeFalsy();
    expect(component.form.errors["name"]).toBeDefined();
  });

  //it('should mark valid', () => {
  //  component.registrationData = validRegistrationInputs[0];
  //  component.form.markAllAsTouched();
  //  expect(component.form.valid).toBeTruthy();
  //});

  //it('should mark valid', () => {
  //  component.registrationData = validRegistrationInputs[0];
  //  component.form.markAllAsTouched();
  //  expect(component.form.valid).toBeTruthy();
  //});

});
