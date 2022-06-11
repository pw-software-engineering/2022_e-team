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

  public isCommon: boolean = false;
  public isDeliverer: boolean = false;
  public isProducer: boolean = false;

  constructor(private router: Router, private registrationService: RegistrationService, private orderService: OrderService) {
  }

  ngOnInit(): void {
    if (this.registrationService.isCommonUser()) {
      this.isCommon = true;
      this.orderService.refreshCartCount();
    }
    else if (this.registrationService.isDeliverer()) {
      this.isDeliverer = true;
    }
    else if (this.registrationService.isProducer()) {
      this.isProducer = true;
    }

    
  }

  public goToClientDiets() {
    this.router.navigate(['/client/diets']);
  }

  public goToClientOrders() {
    this.router.navigate(['/client/orders']);
  }

  public goToClientCart() {
    this.router.navigate(['/client/cart']);
  }

  public goToProducerDiets() {
    this.router.navigate(['/producer/diets']);
  }

  public goToProducerOrders() {
    this.router.navigate(['/producer/orders']);
  }

  public goToDelivererHistory() {
    this.router.navigate(['/deliverer/history']);
  }

  public goToDelivererOrders() {
    this.router.navigate(['/deliverer/orders']);
  }


  public logoutCommon() {
    this.registrationService.logout();
    this.router.navigate(['/client/login']);
  }

  public logoutDeliverer() {
    this.registrationService.logout();
    this.router.navigate(['/deliverer/login']);
  }

  public logoutProducer() {
    this.registrationService.logout();
    this.router.navigate(['/producer/login']);
  }
}
