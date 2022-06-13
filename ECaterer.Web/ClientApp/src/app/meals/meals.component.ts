import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { RegistrationService } from '../registration/api/registration.service';
import { MealsService } from './api/meals.service'
import { mealDto } from './api/mealsDtos';

@Component({
  selector: 'app-meals',
  templateUrl: './meals.component.html',
  styleUrls: ['./meals.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class MealsComponent implements OnInit {

  constructor(private mealsService: MealsService,
    private registrationService: RegistrationService,
    private router: Router,
    private inRoute: ActivatedRoute) {
  }

  private dietId: string;

  public meals: mealDto[];

  ngOnInit(): void {

    this.inRoute.params.subscribe((params: Params) => {
      this.dietId = params['id'];
      this.resolveMeals();
    });
  }

  resolveMeals() {
    this.mealsService.getMeals(this.dietId)
      .then((data) => {
        this.meals = (data as mealDto[]);
        this.meals.forEach(m => {
          m.allergentString = m.allergentList.join(', ');
          m.ingredientString = m.ingredientList.join(', ');
        })
      });
  }

  public logout() {
    this.registrationService.logout();
    this.router.navigate(['/client/login']);
  }
}
