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
import { RouterTestingModule } from '@angular/router/testing';


/* Components */
import { HomeComponent } from '../../home/home.component';
import { ClientRegistration } from '../client-registration/client-registration.component';
import { ClientLogin } from '../client-login/client-login.component';
import { RegistrationService } from '../api/registration.service';
import { ProducerLogin } from './producer-login.component';
import { ILoginData } from '../api/registrationDtos';

describe('WorkerLogin', () => {
  let component: ProducerLogin;
  let fixture: ComponentFixture<ProducerLogin>;

  let validLoginData: ILoginData = {
    email: "root@gmail.com",
    password: "toor",
    userType: 3
  };
  let invalidLoginData: ILoginData = {
    email: "",
    password: "toor",
    userType: 3
  };

  beforeAll(function () {
    jasmine.DEFAULT_TIMEOUT_INTERVAL = 999999;
  });

  var registrationServiceMock = jasmine.createSpyObj('RegistrationService', ['loginWorker']);
  registrationServiceMock.loginWorker.and.returnValue(new Promise<object>((resolve, reject) => { resolve({}); }));

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        HomeComponent,
        ClientRegistration,
        ClientLogin,
        ProducerLogin
      ],
      imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterTestingModule.withRoutes([
          { path: '', component: HomeComponent, pathMatch: 'full' },
          { path: 'client/register', component: ClientRegistration, pathMatch: 'full' },
          { path: 'client/login', component: ClientLogin, pathMatch: 'full' },
          { path: 'worker/login', component: ProducerLogin, pathMatch: 'full' }
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
      providers: [Title,
        {
          provide: RegistrationService,
          useValue: registrationServiceMock
        }],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProducerLogin);
    component = fixture.componentInstance;
    registrationServiceMock.loginWorker.calls.reset();
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('login input validation', () => {

    it('should mark invalid on empty form', () => {
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeFalsy();
    });

    it('should mark valid', () => {
      component.form.setValue(validLoginData);
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeTruthy();
    });

    it('should mark invalid on email', () => {
      component.form.setValue(invalidLoginData);
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeFalsy();
    });
  });

  it('should call loginWorker on valid form', () => {
    component.form.setValue(validLoginData);

    expect(registrationServiceMock.loginWorker).toHaveBeenCalledTimes(0);

    component.submitForm();
    fixture.detectChanges();

    expect(registrationServiceMock.loginWorker).toHaveBeenCalledTimes(1);

  });

  it('should not call loginWorker on invalid form', () => {
    component.form.setValue(invalidLoginData);

    expect(registrationServiceMock.loginWorker).toHaveBeenCalledTimes(0);

    component.submitForm();
    fixture.detectChanges();

    expect(registrationServiceMock.loginWorker).toHaveBeenCalledTimes(0);
  });

});
