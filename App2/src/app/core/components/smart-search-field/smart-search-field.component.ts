import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/merge';
import 'rxjs/add/operator/switchMap';
import 'rxjs/Rx';

import { GenericService } from '../../services/generic.service';
import { EntityReference } from '../../model/entity-reference';
import { EntitiesReferences } from '../../model/entities-references';

@Component({
  selector: 'smart-search-field',
  templateUrl: './smart-search-field.component.html',
  styleUrls: ['./smart-search-field.component.css']
})
export class SmartSearchFieldComponent {
  @Input() model: EntityReference;
  @Output() modelChange = new EventEmitter<EntityReference>();
  @Input() label: string;
  @Input() tip: string;
  @Input() targetService: string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;
  @Input() ignitionMilisseconds: number = 300;
  @Input() ignitionMinChars: number = 2;

  private searching = false;
  private searchFailed = false;
  private hideSearchingWhenUnsubscribed = new Observable(() => () => this.searching = false);

  constructor(private service: GenericService) { }

  format(dto: EntityReference): string {
    return dto == null
      ? ''
      : dto.presentation;
  }

  changeModel(event: any): void {
    if (event && event.autoId) { //verifica se é um objeto válido (pq antes de selecionar é apenas uma string)
      this.model = event;
      this.modelChange.emit(this.model);
    }
  }

  searchAsYouType = (text$: Observable<string>) =>
    text$
      .debounceTime(this.ignitionMilisseconds)
      .distinctUntilChanged()
      .do(() => this.searching = true)
      .switchMap((term: string) => {
        if (!term || term.length < this.ignitionMinChars)
          return new EmptyObservable<EntityReference[]>();
        else
          return this.service.smartSearch(this.targetService, term)
            .flatMap(dto => of(dto.references))
            .do(() => {
              this.searchFailed = false
            })
            .catch(() => {
              this.searchFailed = true;
              return of([]);
            })
      })
      .do(() => this.searching = false)
      .merge(this.hideSearchingWhenUnsubscribed);

  realSearch(smartEntry: string) {
    return this.service.smartSearch(this.targetService, smartEntry);
  }
}

//ref: //https://ng-bootstrap.github.io/#/components/typeahead/examples
//considerações:
//model:EntityReference e targetService:string deixam o componente bem acoplado ao restante do design (o que é ruim)
//tavez uma solução seria não usar o tipo EntityReference e tentar abstrair o uso da propriedade Presentation
//outra parte da solução seria criar um @Output para a pesquisa ser feita de fora
