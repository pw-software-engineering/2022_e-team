import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from "@angular/router";
import { OrderService } from '../order/api/order.service';
import { RegistrationService } from '../registration/api/registration.service';
import { DietDTO } from '../diets/api/dietDTO';
import { OrderDTO } from '../order/api/orderDTO';
import { IAddressData } from '../registration/client-registration/client-registration.component';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class CartComponent implements OnInit {
  constructor(private TitleService: Title, private router: Router, private orderService: OrderService, private registrationService: RegistrationService) {
    this.TitleService.setTitle("Koszyk");
    this.addressForm = new FormGroup({
      street: new FormControl(this.addressData.street, [Validators.required, Validators.maxLength(250), Validators.minLength(4),
      Validators.pattern(this.letterRegWithSpaceInside)]),
      buildingNumber: new FormControl(this.addressData.buildingNumber, [Validators.required, Validators.maxLength(50), Validators.minLength(1),
      Validators.pattern(this.alphaNumReg)]),
      apartmentNumber: new FormControl(this.addressData.apartmentNumber, [Validators.maxLength(50), Validators.pattern(this.alphaNumReg)]),
      postCode: new FormControl(this.addressData.postCode, [Validators.required, Validators.maxLength(5), Validators.minLength(5)]),
      city: new FormControl(this.addressData.city, [Validators.required, Validators.minLength(2), Validators.maxLength(50),
      Validators.pattern(this.letterRegWithSpaceInside)])
    });
  }

  public phoneNumberMask = '+48-000-000-000';
  public postCodeMask = '00-000';
  public passwordReg = '^(?=(.*[A-Z]){1,})(?=(.*[!@#$%^&*()+.]){1,})(?=(.*[0-9]){1,})(?=(.*[a-z]){1,}).{8,25}$';
  public letterReg = '^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]*$';
  public letterRegWithSpaceInside = '^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ ]*[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$';
  public alphaNumReg = '^[a-zA-Z0-9]*$';

  public addressData: IAddressData = {
    street: "",
    buildingNumber: "",
    apartmentNumber: "",
    postCode: "",
    city: ""
  }

  public addressForm: FormGroup;

  public order: OrderDTO = {
    startDate: new Date(),
    endDate: new Date(),
    comment: "",
    dietIds: [],
    address: null
  };

  public diets: DietDTO[];

  public isDefaultAdressChecked: boolean = true;

  public isCustomAdressChecked: boolean = false;

  ngOnInit(): void {
    this.resolveDiets();
  }

  resolveDiets() {
    this.orderService.getDietsInCart()
      .then((data) => {
        this.diets = (data as DietDTO[]);
      });
  }

  goToDiet(dietId: number) {
    this.router.navigate(["client/diets", dietId]);
  }

  clearDietFromOrder(dietId: number) {
    this.orderService.removeDietInCart(dietId);
    this.resolveDiets();
  }

  sendOrder() {
    if (this.isCustomAdressChecked) {
      this.addressForm.markAllAsTouched();
      if (this.addressForm.invalid) return;
      this.order.address = {
        street: this.addressData.street,
        building: this.addressData.buildingNumber,
        apartment: this.addressData.apartmentNumber,
        city: this.addressData.city,
        code: this.addressData.postCode
      }
    }
    this.order.dietIds = this.diets.map(d => d.id);
    this.orderService.sendOrder(this.order)
      .then(() => {
        this.router.navigate(["/"])
      }) 
  }

  public switchToCustomAddress() {
    this.isDefaultAdressChecked = !this.isCustomAdressChecked;

  }

  public switchToDefaultAddress() {
    this.isCustomAdressChecked = !this.isDefaultAdressChecked;
  }
}
