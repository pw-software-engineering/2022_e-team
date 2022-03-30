import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Title } from '@angular/platform-browser';
import { IRegistrationService, RegistrationService } from '../api/registration.service';
import { Router } from "@angular/router";

@Component({
  selector: 'app-client-login',
  templateUrl: './client-login.component.html',
  styleUrls: ['./client-login.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class ClientLogin implements OnInit {

  private registrationService: IRegistrationService;

  public loginData: LoginData = {
    email: "",
    password: ""
  }

  public errorMessage = '';

  public form: FormGroup;

  constructor(private TitleService: Title, private router: Router, private RegistrationService: RegistrationService) {

    this.registrationService = RegistrationService;
    this.TitleService.setTitle("Logowanie");

    this.form = new FormGroup({
      email: new FormControl(this.loginData.email, [Validators.required, Validators.email, Validators.maxLength(250)]),
      password: new FormControl(this.loginData.password, [
        Validators.required, Validators.maxLength(25)])
    });
}

  ngOnInit(): void {
    $("#passwordTextBox input").attr("type", "password");
  }


  submitForm(): void { 
    this.form.markAllAsTouched();
    this.clearError();
    if (this.form.valid) {
      this.registrationService.loginUser()
        .then(() => {
          console.log("Pomyślnie zalogowano.");
        })
        .catch((err) => {
          this.showError(err);
        });
    }
    else {
      this.showError("Niepoprawny adres email lub hasło.");
    }
  }

  redirectToRegister(): void {
    this.router.navigate(['/client/register']);
  }

  clearError() {
    this.errorMessage = "";
  }

  showError(message: string) {
    this.errorMessage = message;
  }
}

interface LoginData {
  email: string,
  password: string
}
