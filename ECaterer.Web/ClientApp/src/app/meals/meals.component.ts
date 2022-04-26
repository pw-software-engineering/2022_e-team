import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegistrationService } from '../registration/api/registration.service';
import { MealsService } from './api/meals.service'
import { mealDto } from './api/mealsDtos'

@Component({
  selector: 'app-home',
  templateUrl: './meals.component.html',
})

export class MealsComponent implements OnInit {

  constructor(private mealsService: MealsService, private registrationService: RegistrationService,private router: Router) {
  }

  public gridItems: any;

  ngOnInit(): void {
    const meals = this.mealsService.getMeals();
    //let mealsTemp: { meal: mealDto }[] = [
    //  { }
    //];
    console.log(meals);
    this.gridItems = [{
      field1: "coca",
      field2: "cola"
    },
    {
      field1: "cheese",
      field2: "burger"
    },
    {
      field1: "pepsi",
      field2: "fanta"
    }]
  }


  public logout() {
    this.registrationService.logout();
    this.router.navigate(['/client/login']);
  }
}
