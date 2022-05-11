import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from "@angular/router";
import { DietsService } from '../api/diets.service';
import { RegistrationService } from '../../registration/api/registration.service';
import { DietDTO } from '../api/dietDTO';

@Component({
  selector: 'app-diets',
  templateUrl: './diets.component.html',
  styleUrls: ['./diets.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class DietsComponent implements OnInit {
  constructor(private TitleService: Title, private router: Router, private dietsService: DietsService, private registrationService: RegistrationService) {
    this.TitleService.setTitle("Lista dostępnych diet");
  }


  public diets: DietDTO[];

  ngOnInit(): void {
    this.dietsService.getDiets()
      .then((data) => {
        this.diets = (data as DietDTO[]);
      });
  }

  goToDiet(dietId: number) {
    this.router.navigate(["client/diets", dietId]);
  }

}
