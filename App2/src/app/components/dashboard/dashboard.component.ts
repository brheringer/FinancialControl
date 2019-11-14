import { Component, OnInit } from '@angular/core';
import { DashboardService } from './dashboard.service';
import { Dashboard } from './dashboard.model';
import { AlertService } from 'src/app/core/local-services/alert.service';
import { ChartService } from './chart-service.service';
import * as moment from 'moment';
import { Entry } from 'src/app/model/entry.model';
import { unescapeIdentifier } from '@angular/compiler';

@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  providers: [DashboardService, ChartService]
})
export class DashboardComponent implements OnInit {

  model: Dashboard = new Dashboard();
  showFilters: boolean = true;
  periodicities: string[] = ['DAILY', 'MONTHLY', 'ANNUALLY', 'TOTAL'];

  constructor(private service: DashboardService, private chartService: ChartService, private alertService: AlertService) { }

  ngOnInit() {
    this.model.filterInitialDate = moment().utc().startOf('day').subtract(3, 'months').toDate();
  }

  generateDashboard(): void {
    
    let filters = this.clone(this.model);
    filters.entries = null;

    this.service.retrieveEntries(filters)
      .subscribe(dto => {
        if (!dto.response.hasException) {
          this.model.entries = dto.entries;
          //TODO por performance, é bom mudar para algo menos analítico

          this.plotChart(dto.entries);
        }
      });
  }

  plotChart(entries: Entry[]): void {
    let step2 = this.transformByPeriodicityOption(entries);
    this.chartService.plotChart('chart', step2);
  }

  transformByPeriodicityOption(entries: Entry[]): Entry[] {
    let output: Entry[] = [];
    for(let entry of entries) {
      let target = output.find(e => e.date.getTime() == this.transformDateByPeriodicity(entry.date).getTime());
      if(target) {
        target.value += entry.value;
      }
      else {
        target = new Entry();
        target.value = entry.value;
        target.date = this.transformDateByPeriodicity(entry.date);
        output.push(target);
      }
    }
    return output;
  }

  transformDateByPeriodicity(date: Date): Date {
    let transformedDate: Date = date;
    const dummyDate = moment().startOf('day').toDate();
    switch(this.model.periodicity) {
      case 'ANNUALLY': { transformedDate = moment(date).startOf('year').toDate(); }
        break;
      case 'DAILY': { transformedDate = moment(date).startOf('day').toDate(); }
        break;
      case 'MONTHLY': { transformedDate = moment(date).startOf('month').toDate();  }
        break;
      case 'TOTAL': { transformedDate = dummyDate; }
        break;
    }
    return transformedDate;
  };

  cloneEntry(obj: Entry): Entry {
    return Object.assign({}, obj); //https://stackoverflow.com/questions/41907903/cloning-objects-typescript
  }

  clone(obj: Dashboard): Dashboard {
    return Object.assign({}, obj); //https://stackoverflow.com/questions/41907903/cloning-objects-typescript
  }
}
