

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class RegistrationService {

  constructor() {
  }

  public registerUser(): Promise<object> {
    return new Promise<object>((resolve, reject) => {
      $.get("someinvalidurl")
        .then((data) => resolve(data))
        .catch((err: any) => {
          switch (err.status) {
            case "501":
              reject(err.responseText);
              break;
            default:
              reject("Wystąpił błąd serwera. Spróbuj później.");
          }
        });
    });
  }

  public loginUser(): Promise<object> {
    return new Promise<object>((resolve, reject) => {
      $.get("someinvalidurl")
        .then((data) => resolve(data))
        .catch((err: any) => {
          switch (err.status) {
            case "501":
              reject(err.responseText);
              break;
            default:
              reject("Wystąpił błąd serwera. Spróbuj później.");
          }
        });
    });
  }

}



