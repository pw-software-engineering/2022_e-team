import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CookieOptions, CookieService } from 'ngx-cookie';
import { mealDto } from './mealsDtos'

@Injectable({
  providedIn: 'root',
})

export class MealsService {

  private commonHeaders = new HttpHeaders();

  private TOKEN_KEY = "SESSIONID";
  private mealsUrl: string;

  // in seconds
  private expireTime: number = 30 * 60;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private cookieService: CookieService) {
    this.mealsUrl = baseUrl + 'Meals/';
    this.commonHeaders.set("Content-Type", "application/json");
   // this.commonHeaders.set("Authorization", "bearer " + cookieService.get(this.TOKEN_KEY));
  }

  public getMeals(): Promise< void | Array<mealDto>> {

    return this.http.get<Array<mealDto>>(this.mealsUrl + "GetMeals", { headers: this.commonHeaders }).toPromise();
  }
 
}



