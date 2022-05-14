import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { OrderService } from '../order/api/order.service';
import { RegistrationService } from '../registration/api/registration.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class Navigation implements OnInit {
  constructor(private router: Router, private registrationService: RegistrationService, private orderService: OrderService) {
  }

  ngOnInit(): void {
    this.orderService.refreshCartCount();
  }

  public goToDiets() {
    this.router.navigate(['/client/diets']);
  }

  public goToCart() {
    this.router.navigate(['/client/cart']);
  }

  public goToDiet() {
  }

  public logout() {
    this.registrationService.logout();
    this.router.navigate(['/client/login']);
  }
}
