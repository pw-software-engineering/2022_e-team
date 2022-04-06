

import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})

export class RegistrationService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  public registerUser(): Promise<object> {
    return new Promise<object>((resolve, reject) => {
      //this.http.post("https://localhost:44330/" + "api/Client/Register", { password: "abcd", client: { email: "s@gmail.com" } })
      //  .subscribe(
      //    (data) => console.log(data),
      //    (err) => console.log(err)
      //  )
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

  public loginWorker(): Promise<object> {
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



