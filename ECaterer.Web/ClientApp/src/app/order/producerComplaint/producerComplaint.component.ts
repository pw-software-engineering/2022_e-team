import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Params, Router } from "@angular/router";
import { OrderService } from '../api/order.service';
import { ComplaintDTO } from '../api/orderDTO';

@Component({
  selector: 'app-producer-complaint',
  templateUrl: './producerComplaint.component.html',
  styleUrls: ['./producerComplaint.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class ProducerComplaintComponent implements OnInit {
  constructor(private TitleService: Title, private router: Router,
    private orderService: OrderService,
    private inRoute: ActivatedRoute) {
    this.TitleService.setTitle("Podgląd zamówienia");
  }

  private orderNumber: string;

  public complaintModel: ComplaintDTO = {
    clientName: "",
    complaintDate: new Date(),
    description: "",
    status: ""
  };


  ngOnInit(): void {
    this.inRoute.params.subscribe((params: Params) => {
      this.orderNumber = params['id'];
      this.resolveComplaint();
    });
  }

  resolveComplaint() {
    this.orderService.getComplaint(this.orderNumber)
      .then((data) => {
        this.complaintModel = data as ComplaintDTO;
        this.complaintModel.complaintDate = new Date(this.complaintModel.complaintDate);
      })
  }
}
