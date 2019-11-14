import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { Entry } from 'src/app/model/entry.model';

declare var google: any;

@Injectable()
export class ChartService {
  constructor() { 
    google.charts.load('current', {'packages':['corechart']});
  }

  public plotChart(elementId: string, entries: Entry[]) {
    let data = this.prepareData(entries);
    var options = this.getOptions();
    this.buildChart(elementId, data, this.getOptions());
  }

  //so pra testes
  prepareData(entries: Entry[]) {

    let data: any[] = [
      ['Date', 'Value', {role: 'style'}]
    ];
    // let odd = '{color:#609BB4;stroke-width:0;opacity:1}';
    // let even = '{color:#3F748B;stroke-width:0;opacity:1}';
    // let suspect = '{color:#3F0000;stroke-width:0;opacity:1}';
    let style = '{color:#000000;opacity:1;stroke-width:1;stroke-color:black';
    entries.map(e => {
      let found = data.find(d => d['Date'] == e.date);
      if(found == null)
        data.push([e.date, e.value, style]);
      else
        found['Value'] += e.value;
    });
    return data;
  }
  
  private getOptions(): any {
    var options = {
        title: 'Totais...',
        'height':300,
        legend: {
          position: "none"
        },
        vAxis: {
          // title: 'R$',
          // gridlines: {
          //   color: 'transparent'
          // },
        },
        // hAxis: {
        //   gridlines: {
        //     color: 'transparent'
        //   }//,
        //   // title: 'Time of Day',
        //   // format: 'h:mm a',
        //   // viewWindow: {
        //   //   min: [7, 30, 0],
        //   //   max: [17, 30, 0]
        //   // }
        // }//,
        // annotations: {
        //   textStyle: {
        //     fontSize: 10,
        //     bold: false,
        //     color: 'white',
        //     auraColor: null,
        //     opacity: 1
        //   }
        // }
      };
      return options;
  }

  //http://anthonygiretti.com/2017/10/12/using-google-charts-in-angular-4-project-part-2/
  protected buildChart(elementId: string, data: any[], options: any) : void {
    var func = (options) => {
      var datatable = google.visualization.arrayToDataTable(data);
      var chart = new google.visualization.ColumnChart(document.getElementById(elementId));
      chart.draw(datatable, options);
    };   
    var callback = () => func(options);
    google.charts.setOnLoadCallback(callback);
  }

}
