import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { RegistrationService } from '../registration/api/registration.service';

@Injectable({ providedIn: "root" })
export class DelivererLoginGuard implements CanActivate {

  constructor(private router: Router, private registrationService: RegistrationService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    if (!this.registrationService.isDeliverer()) {
      return true;
    }

    this.router.navigate(['/deliverer/orders']);
    return false;

  }
}
