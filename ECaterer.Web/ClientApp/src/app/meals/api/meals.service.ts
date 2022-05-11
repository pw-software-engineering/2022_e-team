import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CookieOptions, CookieService } from 'ngx-cookie';
import { mealDto } from './mealsDtos'

@Injectable({
  providedIn: 'root',
})

export class MealsService {

  private commonHeaders = new HttpHeaders();
  private mealsUrl: string;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.mealsUrl = baseUrl + 'Meals/';
    this.commonHeaders.set("Content-Type", "application/json");
  }

  public getMeals(dietId: number): Promise< void | Array<mealDto>> {
    return this.http.get<Array<mealDto>>(this.mealsUrl + "GetMealsInDiet/" + dietId, { headers: this.commonHeaders }).toPromise();
  }
 
}



