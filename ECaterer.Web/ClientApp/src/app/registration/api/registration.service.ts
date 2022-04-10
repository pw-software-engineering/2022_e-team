

import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { loginDto, registerDto, authDto } from './registrationDtos';
import { CookieOptions, CookieService } from 'ngx-cookie';

@Injectable({
  providedIn: 'root',
})

export class RegistrationService {

  private TOKEN_KEY = "SESSIONID";
  private clientUrl: string;

  // in seconds
  private expireTime: number = 20;

  private jwtOptions: CookieOptions = {
    secure: true
  };

  constructor(private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private cookieService: CookieService) {
    this.clientUrl = baseUrl + "client/";
  }

  public registerUser(): Promise<void | authDto> {

    var registerData: registerDto = {
      password: "abcd"
    };

    return this.http.post<authDto>(this.clientUrl + "registeruser", registerData).toPromise()
      .then(
        (data: authDto) => {
          this.refreshExpireJWT();
          this.cookieService.put(this.TOKEN_KEY, data.tokenJWT, this.jwtOptions);
        }
    );
  }

  public loginUser(): Promise<void | authDto> {

    var loginData: loginDto = {
      email: "s@gmail.com",
      password: "abcd"
    };

    return this.http.post<authDto>(this.clientUrl + "loginuser", loginData).toPromise()
      .then(
        (data) => {
          this.refreshExpireJWT();
          this.cookieService.put(this.TOKEN_KEY, data.tokenJWT, this.jwtOptions);
        }
      );
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

  public getToken(): string | null {
    var token = this.cookieService.get(this.TOKEN_KEY);
    return token !== undefined ? token : null;
  }

  public logout() {
    this.cookieService.remove(this.TOKEN_KEY);
}

  private refreshExpireJWT() {
    var targetDate = new Date();
    targetDate.setTime(targetDate.getTime() + this.expireTime * 1000);

    this.jwtOptions.expires = targetDate;
  }
}



