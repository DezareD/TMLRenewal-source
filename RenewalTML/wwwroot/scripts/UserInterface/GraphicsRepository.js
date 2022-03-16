/* IMPORT MODULES*/

import { nFormatter } from '../UserInterface/UtilitsRepository.js';

/* GRAPHICS */

export var graphicsList = new Array();             // Хранит список графиков.

export function AddSingleData(instname, numberofDataSet, obj) {

    var chart = graphicsList[instname];

    if (chart) {

        obj.forEach(model => {
            // вставляем в определенный дата сет информацию
            chart.data.datasets[numberofDataSet].data.push(model.data);

            // создаём новый столбец для нашей вставленный даты в датасеты ранее
            chart.data.labels.push(model.label);
        });

        // обновляем
        chart.update();
    }
}

export function DestroyChart(inst) {
    console.log("chart: ", graphicsList[inst]);
    if (graphicsList[inst]) {
        graphicsList[inst].destroy();
        delete graphicsList[inst];
    }
}

export function GenerateChart(htmltag, inst, tickratekf) {

    if (tickratekf <= 0)
        tickratekf = 1;

    if (inst == "user-profile-balance") {
        var ctx = document.getElementById(htmltag).getContext('2d');

        var gradient = ctx.createLinearGradient(0, 0, 0, 150);
        gradient.addColorStop(0, 'rgba(240,90,40,.3)');
        gradient.addColorStop(1, 'rgba(240,90,40,0)');

        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: [],
                datasets: [
                    {
                        data: [],
                        label: '',
                        fill: 'start',
                        backgroundColor: gradient,
                        pointBorderWidth: 0,
                        pointBorderColor: "transparent",
                        pointBackgroundColor: "transparent",
                        pointHoverBackgroundColor: "#e65727",
                        pointHoverBorderColor: "white",
                        pointHoverBorderWidth: 2,
                        pointHoverRadius: 4,
                        borderColor: "#e65727",
                        borderWidth: 5,
                        tension: 0.2
                    }
                ],

            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        grid: {
                            borderDash: [4, 4],
                            color: "#dde2ec",
                            drawBorder: false
                        },
                        ticks: {
                            stepSize: tickratekf,
                            font: {
                                family: 'SF UI Display',
                                size: 12,
                                color: "#bdc1c5"
                            },
                            callback: function (value, index) {
                                return nFormatter(value) + "   ";
                            }
                        }
                    },
                    x: {
                        grid: {
                            display: false
                        },
                        ticks: {
                            callback: function (value, index) {
                                if (index % 2 == 0) return value
                                else return ""
                            }
                        }
                    }
                },
                plugins: {
                    legend: {
                        display: false
                    },
                    tooltip: {
                        backgroundColor: "white",
                        titleColor: "#333",
                        bodyColor: "#333",
                        displayColors: false,
                        titleFont: 'SF UI Display',
                        titleMarginBottom: 1,
                        titleFont: {
                            weght: "bold"
                        },
                        callbacks: {
                            label: function (context) {
                                var label = context.parsed.x;
                                return label;
                            },
                            title: function (context) {
                                var label = context[0].parsed.y;
                                return nFormatter(label, 2);
                            }
                        }
                    }
                }
            }
        });

        graphicsList["user-profile-balance"] = myChart;
    }
    else {
        console.log("# INFO: График для рендеринга не найден.");
    }
}