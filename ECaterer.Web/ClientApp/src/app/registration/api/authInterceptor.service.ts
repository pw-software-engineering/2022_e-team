import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { RegistrationService } from "./registration.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private registrationService: RegistrationService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    var token = this.registrationService.getToken();

    if (token !== null) {
      const modifiedReq = req.clone({
        headers: req.headers.set('api-key', `${token}`),
      });
      return next.handle(modifiedReq);
    }
    return next.handle(req);
  }
}
