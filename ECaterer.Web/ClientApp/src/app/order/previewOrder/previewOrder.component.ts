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
    status: ""
  };

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
      })
  }

}
