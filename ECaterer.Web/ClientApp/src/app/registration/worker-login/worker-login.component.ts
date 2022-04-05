import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Title } from '@angular/platform-browser';
import { RegistrationService } from '../api/registration.service';
import { Router } from "@angular/router";

@Component({
  selector: 'app-worker-login',
  templateUrl: './worker-login.component.html',
  styleUrls: ['./worker-login.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class WorkerLogin implements OnInit {

  public loginData: ILoginData = {
    login: "",
    password: ""
  }

  public errorMessage = '';

  public form: FormGroup;

  constructor(private TitleService: Title, private router: Router, private registrationService: RegistrationService) {

    this.TitleService.setTitle("Logowanie");

    this.form = new FormGroup({
      login: new FormControl(this.loginData.login, [Validators.required, Validators.minLength(1), Validators.maxLength(250)]),
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
      this.registrationService.loginWorker()
        .then(() => {
          console.log("Pomyślnie zalogowano.");
        })
        .catch((err) => {
          this.showError(err);
        });
    }
    else {
      this.showError("Niepoprawny login lub hasło.");
    }
  }

  clearError() {
    this.errorMessage = "";
  }

  showError(message: string) {
    this.errorMessage = message;
  }
}

export interface ILoginData {
  login: string,
  password: string
}
