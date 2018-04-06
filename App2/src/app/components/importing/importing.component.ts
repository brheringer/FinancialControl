import { Component, OnInit } from '@angular/core';

import { Importing } from '../../model/importing.model';
import { ImportingService } from '../../services/importing.service';
import { AlertService } from '../../core/local-services/alert.service';

@Component({
  selector: 'importing',
  templateUrl: './importing.component.html',
  styleUrls: ['./importing.component.css'],
  providers: [ImportingService]
})
export class ImportingComponent implements OnInit {

  public model: Importing = new Importing();

  constructor(
    private service: ImportingService,
    private alertService: AlertService) { }

  ngOnInit() {
  }

  import(): void {
    var input: any = document.getElementById('fieldFilePath');
    var fileList = input.files;
    this.service.import(fileList[0])
      .subscribe(dto => {
        if (dto.response.hasException) {
          this.model.success = false;
          this.model.errorsMessages = [dto.response.exception];
          this.model.processed = true;
          this.alertService.error(dto.response.exception);
        }
        else {
          if (dto.success) {
            var msg = `${dto.quantityOfImportedEntries} entries imported successfully @ ${dto.timeStamp}`;
            this.model.errorsMessages = [msg];
            this.model.success = true;
            this.model.processed = true;
          }
          else {
            this.model.errorsMessages = dto.errorsMessages;
            this.model.success = false;
            this.model.processed = true;
          }
        }
      });
  }

}
