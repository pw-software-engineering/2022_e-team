import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Params, Router } from "@angular/router";
import { OrderService } from '../api/order.service';
import { PreviewOrderDTO } from '../api/orderDTO';

@Component({
  selector: 'app-preview-order',
  templateUrl: './previewOrder.component.html',
  styleUrls: ['./previewOrder.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class PreviewOrderComponent implements OnInit {
  constructor(private TitleService: Title, private router: Router,
    private orderService: OrderService,
    private inRoute: ActivatedRoute) {
    this.TitleService.setTitle("Podgląd zamówienia");
  }

  public mealsInOrderShown: boolean = false;

  private orderNumber: string;

  public editModel: PreviewOrderDTO = {
    orderNumber: "",
    dietNames: [],
    cost: 0,
    address: "",
    deliverDate: new Date(),
    orderDate: new Date(),
    comment: "",
    hasComplaint: false,
    phone: "",
    status: "",
    mealsConcatenated: []
  };

  public isReadyToDeliver: boolean = false;

  ngOnInit(): void {
    this.inRoute.params.subscribe((params: Params) => {
      this.orderNumber = params['id'];
      this.resolveOrder();
    });
  }

  resolveOrder() {
    this.orderService.previewOrder(this.orderNumber)
      .then((data) => {
        this.editModel = data as PreviewOrderDTO;
        this.editModel.orderDate = new Date(this.editModel.orderDate);
        this.editModel.deliverDate = new Date(this.editModel.deliverDate);
        this.isReadyToDeliver = this.editModel.status == "ToRealized";
      })
  }

  redirectDeliverer() {
    this.orderService.sendOrderToDeliverer(this.orderNumber)
      .then(() => {
        this.router.navigate(["producer/orders"]);
      })
  }

  showComplaint() {
    this.router.navigate([`producer/orders/${this.orderNumber}/complaint`]);
  }

  showMealsInOrder() {
    this.mealsInOrderShown = true;
  }

  closeMealsInOrder() {
    this.mealsInOrderShown = false;
  }
}
