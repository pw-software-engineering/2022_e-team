import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegistrationService } from '../registration/api/registration.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent implements OnInit {

  constructor(private registrationService: RegistrationService, private router: Router) {
  }

  public gridItems: any;

    ngOnInit(): void {
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
