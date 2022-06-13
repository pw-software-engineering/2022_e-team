import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Title } from '@angular/platform-browser';
import { RegistrationService } from '../api/registration.service';
import { Router } from "@angular/router";
import { ILoginData } from '../api/registrationDtos';

@Component({
  selector: 'app-client-login',
  templateUrl: './client-login.component.html',
  styleUrls: ['./client-login.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class ClientLogin implements OnInit {

  public loginData: ILoginData = {
    email: "",
    password: "",
    userType: 1
  }

  public errorMessage = '';

  public form: FormGroup;

  constructor(private TitleService: Title, private router: Router, private registrationService: RegistrationService) {

    this.TitleService.setTitle("Logowanie");

    this.form = new FormGroup({
      email: new FormControl(this.loginData.email, [Validators.required, Validators.email, Validators.maxLength(250)]),
      password: new FormControl(this.loginData.password, [
        Validators.required, Validators.minLength(1), Validators.maxLength(25)])
    });
}

  ngOnInit(): void {
    $("#passwordTextBox input").attr("type", "password");
  }


  submitForm(): void { 
    this.form.markAllAsTouched();
    this.clearError();
    if (this.form.valid) {
      this.registrationService.login(this.loginData)
        .then(() => {
          this.router.navigate(['client/diets']);
        })
        .catch((err) => {
          if (err.status == 401)
            this.showError("Nieprawidłowy mail lub hasło");
          else
            this.showError("Wystąpił błąd serwera. Spróbuj ponownie później.");
        });
    }
    else {
      this.showError("Niepoprawny adres email lub hasło.");
    }
  }

  redirectToRegister(): void {
    this.router.navigate(['client/register']);
  }

  clearError() {
    this.errorMessage = "";
  }

  showError(message: string) {
    this.errorMessage = message;
  }
}
