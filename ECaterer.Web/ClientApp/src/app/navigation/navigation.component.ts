import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { RegistrationService } from '../registration/api/registration.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class Navigation implements OnInit {
  constructor(private router: Router, private registrationService: RegistrationService) {
  }

  ngOnInit(): void {
   
  }

  public goToDiets() {
    window.location.reload();
  }

  public goToCart() {
  }

  public goToDiet() {
  }

  public logout() {
    this.registrationService.logout();
    this.router.navigate(['/client/login']);
  }
}
