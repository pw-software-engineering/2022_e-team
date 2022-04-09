import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { TextBoxComponent } from "@progress/kendo-angular-inputs";
import { Title } from '@angular/platform-browser';
import { RegistrationService } from '../api/registration.service';
import { Router } from "@angular/router";

@Component({
  selector: 'app-client-registration',
  templateUrl: './client-registration.component.html',
  styleUrls: ['./client-registration.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class ClientRegistration implements OnInit {

  public secondTab = false;
  public secondTabAlreadyShown = false;

  public phoneNumberMask = '+48-000-000-000';
  public postCodeMask = '00-000';
  public passwordReg = '^(?=(.*[A-Z]){1,})(?=(.*[!@#$%^&*()+.]){1,})(?=(.*[0-9]){1,})(?=(.*[a-z]){1,}).{8,25}$';
  public letterReg = '^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]*$';
  public letterRegWithSpaceInside = '^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ ]*[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$';
  public alphaNumReg = '^[a-zA-Z0-9]*$'

  public registrationData: IRegistrationData = {
    name: "",
    surname: "",
    email: "",
    password: "",
    phone: ""
  }

  public addressData: IAddressData = {
    street: "",
    buildingNumber: "",
    apartmentNumber: "",
    postCode: "",
    city: ""
  }

  public errorMessage = '';

  public form: FormGroup;
  public addressForm: FormGroup;

  constructor(private TitleService: Title, private router: Router, private registrationService: RegistrationService) {

    this.TitleService.setTitle("Rejestracja");

    this.form = new FormGroup({
      name: new FormControl(this.registrationData.name, [Validators.required,
        Validators.maxLength(50), Validators.minLength(2), Validators.pattern(this.letterReg)]),
      surname: new FormControl(this.registrationData.surname, [Validators.required,
        Validators.maxLength(50), Validators.minLength(2), Validators.pattern(this.letterReg)]),
      email: new FormControl(this.registrationData.email, [Validators.required, Validators.email, Validators.maxLength(250)]),
      password: new FormControl(this.registrationData.password, [
        Validators.required, Validators.maxLength(25), Validators.pattern(this.passwordReg)]),
      phone: new FormControl(this.registrationData.phone, [Validators.required, Validators.maxLength(20)])
    });

    this.addressForm = new FormGroup({
      street: new FormControl(this.addressData.street, [Validators.required, Validators.maxLength(250), Validators.minLength(4),
        Validators.pattern(this.letterRegWithSpaceInside)]),
      buildingNumber: new FormControl(this.addressData.buildingNumber, [Validators.required, Validators.maxLength(50), Validators.minLength(1),
        Validators.pattern(this.alphaNumReg)]),
      apartmentNumber: new FormControl(this.addressData.apartmentNumber, [Validators.maxLength(50), Validators.pattern(this.alphaNumReg)]),
      postCode: new FormControl(this.addressData.postCode, [Validators.required, Validators.maxLength(5), Validators.minLength(5)]),
      city: new FormControl(this.addressData.city, [Validators.required, Validators.minLength(2), Validators.maxLength(50),
        Validators.pattern(this.letterRegWithSpaceInside)])
    });
}

  ngOnInit(): void {
    $("#passwordTextBox input").attr("type", "password");
  }

  moveToAddressTab() {
    this.form.markAllAsTouched();
    this.clearError();
    if (this.form.valid) {
      this.secondTab = true;
      this.secondTabAlreadyShown = true;
    }
    else {
      this.showError("Niepoprawne dane.");
    }
  }

  submitForm(): void {
    this.addressForm.markAllAsTouched();
    this.clearError();
    if (this.addressForm.valid) {
      this.registrationService.registerUser()
        .then(() => {
          console.log("Pomyślnie zarejestrowano.");
        })
        .catch((err) => {
          this.showError(err);
        });
    }
    else {
      this.showError("Niepoprawne dane.");
    }
  }

  backToUserForm(): void {
    this.secondTab = false;
  }


  redirectToLogin(): void {
    this.router.navigate(['/client/login']);
  }

  clearError() {
    this.errorMessage = "";
  }

  showError(message: string) {
    this.errorMessage = message;
  }
}

export interface IRegistrationData {
  name: string,
  surname: string,
  email: string,
  password: string,
  phone: string
}

export interface IAddressData {
  street: string,
  buildingNumber: string,
  apartmentNumber: string,
  postCode: string,
  city: string
}
