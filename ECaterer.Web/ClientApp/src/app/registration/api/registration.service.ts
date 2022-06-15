

import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { loginDto, registerDto, authDto, ILoginData } from './registrationDtos';
import { CookieOptions, CookieService } from 'ngx-cookie';
import { IAddressData, IRegistrationData } from '../client-registration/client-registration.component';
import { User } from 'oidc-client';

@Injectable({
  providedIn: 'root',
})

export class RegistrationService {

  private commonHeaders = new HttpHeaders();

  private TOKEN_KEY = "SESSIONID";
  private USER_TYPE = "USER_TYPE";
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
          this.cookieService.put(this.USER_TYPE, "CommonUser", this.getNewJWTOptions());
        }
    );
  }

  public login(loginData: ILoginData): Promise<void | authDto> {

    var loginData: loginDto = {
      email: loginData.email,
      password: loginData.password,
      userType: loginData.userType
    };

    return this.http.post<authDto>(this.clientUrl + "loginall", loginData, { headers: this.commonHeaders }).toPromise()
      .then(
        (data) => {
          this.cookieService.put(this.TOKEN_KEY, data.tokenJWT, this.getNewJWTOptions());
          var userType: string;
          switch (loginData.userType)
            {
            case 1:
              userType = "CommonUser";
              break;
            case 2:
              userType = "Deliverer";
              break;
            case 3:
              userType = "Producer";
              break;
          }
          this.cookieService.put(this.USER_TYPE, userType, this.getNewJWTOptions());
        }
      );
  }

  public getToken(): string | null {
    var token = this.cookieService.get(this.TOKEN_KEY);
    return token !== undefined ? token : null;
  }

  public logout() {
    this.cookieService.remove(this.TOKEN_KEY);
    this.cookieService.remove(this.USER_TYPE);
}

  private getNewJWTOptions() {
    var targetDate = new Date();
    targetDate.setTime(targetDate.getTime() + this.expireTime * 1000);

     var jwtOptions: CookieOptions = {
       secure: false,
       expires: targetDate
    };

    return jwtOptions;
  }

  public isCommonUser() {
    return this.cookieService.get(this.USER_TYPE) === "CommonUser";
  }

  public isDeliverer() {
    return this.cookieService.get(this.USER_TYPE) === "Deliverer";
  }

  public isProducer() {
    return this.cookieService.get(this.USER_TYPE) === "Producer";
  }
}



