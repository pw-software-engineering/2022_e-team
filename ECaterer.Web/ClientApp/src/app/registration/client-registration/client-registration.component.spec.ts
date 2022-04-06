import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BrowserModule, By, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
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
import { ClientRegistration, IRegistrationData, IAddressData } from '../client-registration/client-registration.component';
import { ClientLogin } from '../client-login/client-login.component';
import { RegistrationService } from '../api/registration.service';

describe('ClientRegistration', () => {
  let component: ClientRegistration;
  let fixture: ComponentFixture<ClientRegistration>;

  beforeAll(function () {
    jasmine.DEFAULT_TIMEOUT_INTERVAL = 999999;
  });

  var registrationServiceMock = jasmine.createSpyObj('RegistrationService', ['registerUser']);
  registrationServiceMock.registerUser.and.returnValue(new Promise<object>((resolve, reject) => { resolve({}); }));

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
        }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientRegistration);
    component = fixture.componentInstance;
    registrationServiceMock.registerUser.calls.reset();
    fixture.detectChanges();
  });

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

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('user input validation', () => {

    it('should mark invalid on empty form', () => {
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeFalsy();
    });

    it('should mark valid', () => {
      component.form.setValue(validRegistrationInputs[0]);
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeTruthy();
    });

    it('should mark invalid on name', () => {
      component.form.setValue(invalidRegistrationInputs[0]);
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeFalsy();
      expect(component.form.controls.name.valid).toBeFalsy();
    });

    it('should mark invalid on surname', () => {
      component.form.setValue(invalidRegistrationInputs[1]);
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeFalsy();
      expect(component.form.controls.surname.valid).toBeFalsy();
    });

    it('should mark invalid on email', () => {
      component.form.setValue(invalidRegistrationInputs[2]);
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeFalsy();
      expect(component.form.controls.email.valid).toBeFalsy();
    });

    describe('password validation', () => {

      it('should mark invalid on password : no special char', () => {
        component.form.setValue(invalidRegistrationInputs[3]);
        component.form.markAllAsTouched();
        expect(component.form.valid).toBeFalsy();
        expect(component.form.controls.password.valid).toBeFalsy();
      });

      it('should mark invalid on password : no capital char', () => {
        component.form.setValue(invalidRegistrationInputs[4]);
        component.form.markAllAsTouched();
        expect(component.form.valid).toBeFalsy();
        expect(component.form.controls.password.valid).toBeFalsy();
      });

      it('should mark invalid on password : no number', () => {
        component.form.setValue(invalidRegistrationInputs[5]);
        component.form.markAllAsTouched();
        expect(component.form.valid).toBeFalsy();
        expect(component.form.controls.password.valid).toBeFalsy();
      });

      it('should mark invalid on password: no small char', () => {
        component.form.setValue(invalidRegistrationInputs[6]);
        component.form.markAllAsTouched();
        expect(component.form.valid).toBeFalsy();
        expect(component.form.controls.password.valid).toBeFalsy();
      });
    });
    it('should mark invalid on phone', () => {
      component.form.setValue(invalidRegistrationInputs[7]);
      component.form.markAllAsTouched();
      expect(component.form.valid).toBeFalsy();
      expect(component.form.controls.phone.valid).toBeFalsy();
    });
  });

  it('should show address form on valid user form', () => {
    component.form.setValue(validRegistrationInputs[0]);
    expect(fixture.debugElement.query(By.css('#registrationForm.addressInfo'))).toBeNull();
    component.moveToAddressTab();
    fixture.detectChanges();
    expect(fixture.debugElement.query(By.css('#registrationForm.addressInfo')).nativeElement).toBeTruthy();
  });

  it('should not show address form on invalid user form', () => {
    component.form.setValue(invalidRegistrationInputs[0]);
    component.moveToAddressTab();
    fixture.detectChanges();
    expect(fixture.debugElement.query(By.css('#registrationForm.addressInfo'))).toBeNull();
  });

  let validAddressData: IAddressData[] = [
    {
      street: "Długa",
      buildingNumber: "1",
      apartmentNumber: "79",
      postCode: "01095",
      city: "Warszawa"
    },
    {
      street: "Powstania Warszawskiego",
      buildingNumber: "1A",
      apartmentNumber: "",
      postCode: "01095",
      city: "Nowy Sącz"
    }
  ];

  let invalidAddressData: IAddressData[] = [
    // invalid on street
    {
      street: "Długa 1",
      buildingNumber: "1",
      apartmentNumber: "",
      postCode: "01095",
      city: "Warszawa"
    },
    // invalid on buildingNumber
    {
      street: "Długa",
      buildingNumber: "",
      apartmentNumber: "",
      postCode: "01095",
      city: "Warszawa"
    },
    // invalid on appartmentNumber
    {
      street: "Długa",
      buildingNumber: "1",
      apartmentNumber: " ",
      postCode: "01095",
      city: "Warszawa"
    },
    // invalid on postCode
    {
      street: "Długa",
      buildingNumber: "1",
      apartmentNumber: "",
      postCode: "0109",
      city: "Warszawa"
    },
    // invalid on city
    {
      street: "Długa",
      buildingNumber: "1",
      apartmentNumber: "",
      postCode: "01095",
      city: "Warszawa "
    }
  ];

  describe('address input validation', () => {

    it('should mark invalid on empty form', () => {
      component.addressForm.markAllAsTouched();
      expect(component.addressForm.valid).toBeFalsy();
    });

    it('should mark valid', () => {
      component.addressForm.setValue(validAddressData[0]);
      component.addressForm.markAllAsTouched();
      expect(component.addressForm.valid).toBeTruthy();
    });

    it('should mark valid with spaces in street and city', () => {
      component.addressForm.setValue(validAddressData[1]);
      component.addressForm.markAllAsTouched();
      expect(component.addressForm.valid).toBeTruthy();
    });

    it('should mark invalid on street', () => {
      component.addressForm.setValue(invalidAddressData[0]);
      component.addressForm.markAllAsTouched();
      expect(component.addressForm.valid).toBeFalsy();
      expect(component.addressForm.controls.street.valid).toBeFalsy();
    });

    it('should mark invalid on buildingNumber', () => {
      component.addressForm.setValue(invalidAddressData[1]);
      component.addressForm.markAllAsTouched();
      expect(component.addressForm.valid).toBeFalsy();
      expect(component.addressForm.controls.buildingNumber.valid).toBeFalsy();
    });

    it('should mark invalid on appartmentNumber', () => {
      component.addressForm.setValue(invalidAddressData[2]);
      component.addressForm.markAllAsTouched();
      expect(component.addressForm.valid).toBeFalsy();
      expect(component.addressForm.controls.apartmentNumber.valid).toBeFalsy();
    });

    it('should mark invalid on postCode', () => {
      component.addressForm.setValue(invalidAddressData[3]);
      component.addressForm.markAllAsTouched();
      expect(component.addressForm.valid).toBeFalsy();
      expect(component.addressForm.controls.postCode.valid).toBeFalsy();
    });

    it('should mark invalid on city', () => {
      component.addressForm.setValue(invalidAddressData[4]);
      component.addressForm.markAllAsTouched();
      expect(component.addressForm.valid).toBeFalsy();
      expect(component.addressForm.controls.city.valid).toBeFalsy();
    });
  });
  it('should call registerUser on valid address form', () => {
    component.addressForm.setValue(validAddressData[0]);

    expect(registrationServiceMock.registerUser).toHaveBeenCalledTimes(0);

    component.submitForm();
    fixture.detectChanges();

    expect(registrationServiceMock.registerUser).toHaveBeenCalledTimes(1);

  });

  it('should not call registerUser on invalid address user form', () => {
    component.addressForm.setValue(invalidAddressData[0]);

    expect(registrationServiceMock.registerUser).toHaveBeenCalledTimes(0);

    component.submitForm();
    fixture.detectChanges();

    expect(registrationServiceMock.registerUser).toHaveBeenCalledTimes(0);
  });

});

