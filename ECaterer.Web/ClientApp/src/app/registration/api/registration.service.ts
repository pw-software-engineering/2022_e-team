

import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { loginDto, registerDto, authDto } from './registrationDtos';
import { CookieOptions, CookieService } from 'ngx-cookie';
import { ILoginData } from '../client-login/client-login.component';
import { IAddressData, IRegistrationData } from '../client-registration/client-registration.component';

@Injectable({
  providedIn: 'root',
})

export class RegistrationService {

  private commonHeaders = new HttpHeaders();

  private TOKEN_KEY = "SESSIONID";
  private clientUrl: string;

  // in seconds
  private expireTime: number = 30 * 60;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private cookieService: CookieService) {
    this.clientUrl = baseUrl + "client/";
    this.commonHeaders.set("Content-Type", "application/json");
  }

  public registerUser(registrationData: IRegistrationData, addressData: IAddressData): Promise<void | authDto> {

    var registerData: registerDto = {
      password: registrationData.password,
      client: {
        firstName: registrationData.name,
        lastName: registrationData.surname,
        phone: registrationData.phone,
        email: registrationData.email
      },
      address: {
        street: addressData.street,
        building: addressData.buildingNumber,
        apartment: addressData.apartmentNumber,
        city: addressData.city,
        code: addressData.postCode
      }
    };

    return this.http.post<authDto>(this.clientUrl + "registeruser", registerData, { headers: this.commonHeaders }).toPromise()
      .then(
        (data: authDto) => {
          this.cookieService.put(this.TOKEN_KEY, data.tokenJWT, this.getNewJWTOptions());
        }
    );
  }

  public loginUser(loginData: ILoginData): Promise<void | authDto> {

    var loginData: loginDto = {
      email: loginData.email,
      password: loginData.password
    };

    return this.http.post<authDto>(this.clientUrl + "loginuser", loginData, { headers: this.commonHeaders }).toPromise()
      .then(
        (data) => {
          this.cookieService.put(this.TOKEN_KEY, data.tokenJWT, this.getNewJWTOptions());
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

  private getNewJWTOptions() {
    var targetDate = new Date();
    targetDate.setTime(targetDate.getTime() + this.expireTime * 1000);

     var jwtOptions: CookieOptions = {
       secure: true,
       expires: targetDate
    };

    return jwtOptions;
  }
}



