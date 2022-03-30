

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class RegistrationService implements IRegistrationService {

  constructor() {
  }

  public registerUser(): void {
    console.log("register");
  }

  public loginUser(): void {
    console.log("login");
  }

}

export interface IRegistrationService {
  registerUser(): void,
  loginUser(): void
}



