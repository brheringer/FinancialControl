import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { AlertService } from '../core/local-services/alert.service';
import { AccountsTotalizationsReportService } from '../services/accounts-totalizations-report.service';
import { AccountsTotalizationsReport } from '../model/accounts-totalizations-report.model';
import {NgxChartsModule} from '@swimlane/ngx-charts';
import { AccountTotalization } from '../model/account-totalization.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  providers: [AccountsTotalizationsReportService]
})
export class DashboardComponent implements OnInit {

  period1ini: Date = moment('01/02/2018', 'DD/MM/YYYY', true).toDate();
  period1end: Date = moment('01/02/2018', 'DD/MM/YYYY', true).toDate();
  period2ini: Date = moment('01/03/2018', 'DD/MM/YYYY', true).toDate();
  period2end: Date = moment('01/03/2018', 'DD/MM/YYYY', true).toDate();
  level: number = 4;
  test: any[];

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  constructor(
    private service: AccountsTotalizationsReportService,
    private alertService: AlertService) { }

  ngOnInit() {
    let filters = new AccountsTotalizationsReport();
    filters.filterInitialDate = this.period1ini;
    filters.filterFinalDate = this.period1end;
    this.service.generateReport(filters).subscribe(totals => {
      if(totals.response.hasException) {
        this.alertService.error(totals.response.exception);
      }
      else {
        this.test = this.wrap(totals);
      }
    });
  }

  wrap(totals: AccountsTotalizationsReport): any[] {
    let data = [];
    for(let t of totals.accountsTotalizations) {
      if(t.accountLevel == this.level) {
        let item = { 
          "name": t.accountDescription,
          "series": [ 
            {"name": "1st Period", "value": t.total}, 
            {"name": "2nd Period", "value": t.total} //TODO
          ]
        }
        data.push(item);
      }
    }
    return data;
  }

}
