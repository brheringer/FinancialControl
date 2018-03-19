import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../core/local-services/alert.service';
import { AccountsTotalizationsReportService } from '../../services/accounts-totalizations-report.service';
import { AccountsTotalizationsReport } from '../../model/accounts-totalizations-report.model';
import { AccountTotalization  } from '../../model/account-totalization.model';
import { Detail } from '../../model/detail.model';

@Component({
  selector: 'accounts-totalizations-report',
  templateUrl: './accounts-totalizations-report.component.html',
  styleUrls: ['./accounts-totalizations-report.component.css'],
  providers: [AccountsTotalizationsReportService]
})
export class AccountsTotalizationsReportComponent implements OnInit {

  model: AccountsTotalizationsReport = new AccountsTotalizationsReport();
  showFilters: boolean = true;

  constructor(
    private service: AccountsTotalizationsReportService,
    private alertService: AlertService) { }

  ngOnInit() {
  }

  generateReport(): void {
    this.service.generateReport(this.model)
      .subscribe(dto => {
        if (dto.response.hasException) {
          this.alertService.error(dto.response.exception);
        }
        else {
          this.model.accountsTotalizations = dto.accountsTotalizations;
        }
      });
  }

  expand(detail: Detail): void {
    //if (detail && detail.originalEntry && detail.originalEntry.autoId > 0)
  }

}

//TODO filtro por nivel, conta, centro
//TODO no analitico, destacar os detalhes de alguma forma
//TODO rever cores das linhas, talvez degrade por nivel e alternar cores no nivel de detalhe e nas linhas analiticas
