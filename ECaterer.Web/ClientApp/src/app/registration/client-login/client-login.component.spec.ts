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
import { ClientLogin, ILoginData } from '../client-login/client-login.component';
import { RegistrationService } from '../api/registration.service';

describe('ClientLogin', () => {
  let component: ClientLogin;
  let fixture: ComponentFixture<ClientLogin>;

  let validMailData: ILoginData = {
    email: "a@gmail.com",
    password: "1"
  };
  let invalidMailData: ILoginData = {
    email: "agmail.com",
    password: "1"
  };

  beforeAll(function () {
    jasmine.DEFAULT_TIMEOUT_INTERVAL = 999999;
  });

  var registrationServiceMock = jasmine.createSpyObj('RegistrationService', ['loginUser']);
  registrationServiceMock.loginUser.and.returnValue(new Promise<object>((resolve, reject) => { resolve({}); }));

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
        RouterTestingModule.withRoutes([
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
      providers: [Title,
        {
          provide: RegistrationService,
          useValue: registrationServiceMock
        }],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientLogin);
    component = fixture.componentInstance;
    registrationServiceMock.loginUser.calls.reset();
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
      component.form.setValue(validMailData);
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeTruthy();
    });

    it('should mark invalid on email', () => {
      component.form.setValue(invalidMailData);
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeFalsy();
    });
  });

  it('should call loginUSer on valid form', () => {
    component.form.setValue(validMailData);

    expect(registrationServiceMock.loginUser).toHaveBeenCalledTimes(0);

    component.submitForm();
    fixture.detectChanges();

    expect(registrationServiceMock.loginUser).toHaveBeenCalledTimes(1);

  });

  it('should not call loginUser on invalid form', () => {
    component.form.setValue(invalidMailData);

    expect(registrationServiceMock.loginUser).toHaveBeenCalledTimes(0);

    component.submitForm();
    fixture.detectChanges();

    expect(registrationServiceMock.loginUser).toHaveBeenCalledTimes(0);
  });

});
