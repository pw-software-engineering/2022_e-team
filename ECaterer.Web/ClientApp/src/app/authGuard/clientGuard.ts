import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { RegistrationService } from '../registration/api/registration.service';

@Injectable({ providedIn: "root" })
export class ClientGuard implements CanActivate {

  constructor(private router: Router, private registrationService: RegistrationService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    if (this.registrationService.isCommonUser()) {
      return true;
    }

    this.router.navigate(['/client/login']);
    return false;

  }
}
