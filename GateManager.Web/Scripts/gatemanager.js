$(document).ready(function() {
    $.validate({modules : 'date' });
});

function flight(data)
{
    var self = this;
    self.flightcode = data.FlightCode;
    self.arrivaltime = ko.observable(data.ArrivalTime);
    self.departuretime = ko.observable(data.DepartureTime );
    self.status = ko.observable(data.Status);
    self.error = ko.observable();
}

function gate(data)
{
    var self = this;
    self.gatenumber = data.GateNumber;
}

function flightModel()  {
    var self = this;

    var apiurl = "http://localhost:56641/";
    self.gates = ko.observableArray();
    self.alternateGates = ko.observableArray();
    self.flights = ko.observableArray();

    self.selectedGate = ko.observable();
    self.selectedFlight = ko.observable();

    self.statusDescription = function( status ){
        var statuses = ['Scheduled', 'Conflicted'];
        return statuses[ status() ];
    }

    self.formattedTime = function( flighttime )
    {
        return moment(flighttime()).format('HH:mm');
    }

    self.loadGates = function() {
        $.getJSON(apiurl + 'api/gates', function (gateInfo) {
            var mappedGates = $.map(gateInfo, function (item) { return new gate(item)});
            self.gates(mappedGates);
        });
    }

    self.loadFlights = function(gate) {
        $.getJSON(apiurl + '/api/gates/' + gate.gatenumber + '/flights', function (flightInfo) {
            var mappedFlights = $.map(flightInfo, function (item) { return new flight( {FlightCode: item.FlightCode, ArrivalTime: moment(item.ArrivalTime).format('HH:mm'), DepartureTime: moment(item.DepartureTime).format('HH:mm'), Status: item.Status}) });
            self.flights(mappedFlights);
        });
        self.selectedGate(gate);
        self.cancelAddFlight();
    }

    self.initFlight = function(){
        var arrival = moment();
        var depart = moment(arrival).add(29, 'minutes');

        return new flight({Flightcode:'', ArrivalTime : arrival.format('HH:mm'), DepartureTime : depart.format('HH:mm'), Status:0});
    }

    self.showAddFlight = function(data) {
        $('#addFlight').show('highlight', {}, 700, null);
    }

    self.cancelAddFlight = function(data) {
        $('#addFlight').hide();
    }

    self.addFlight = function(data) {
        $.ajax({
            headers: {'Accept': 'application/json', 'Content-Type': 'application/json'},
            url: apiurl + 'api/gates/' + self.selectedGate().gatenumber + '/flights',
            type: 'POST',
            data: JSON.stringify({flightcode:data.flightcode, arrivaltime: moment(data.arrivaltime(), "HH:mm"), departuretime: moment(data.departuretime(), "HH:mm"), status: 0}),
            success: function (data) {
                self.loadFlights(self.selectedGate());
                self.cancelAddFlight();
            },
            error: function(xhr, ajaxOptions,thrownError){
                var response = JSON.parse(xhr.responseText);
                $('#adderror').html(response.ExceptionMessage);
            }
        });
    };

    self.cancelFlight = function(data) {
        $.ajax({
            headers: {'Accept': 'application/json', 'Content-Type': 'application/json'},
            url: apiurl + 'api/gates/' + self.selectedGate().gatenumber + '/flights/' + data.flightcode,
            type: 'DELETE',
            success: function (data) {
                self.loadFlights(self.selectedGate());
            },
            error: function(xhr, ajaxOptions,thrownError){
                var response = JSON.parse(xhr.responseText);
            }
        })
    };

    self.changeTime = function(data) {
        $.validate({modules : 'date' });

        $.ajax({
            headers: {'Accept': 'application/json', 'Content-Type': 'application/json'},
            url: apiurl + 'api/gates/' + self.selectedGate().gatenumber + '/flights',
            type: 'PUT',
            data: JSON.stringify({flightcode:data.flightcode, arrivaltime: moment(data.arrivaltime(), "HH:mm"), departuretime: moment(data.departuretime(), "HH:mm"), status: data.status}),
            success: function (data) {
                self.loadFlights(self.selectedGate());
            }
        });
    };

    self.loadAlternateGates = function(data) {
        self.selectedFlight = data;
        $.getJSON(apiurl + 'api/gates/' + self.selectedGate().gatenumber + '/flights/' + data.flightcode + '/alternate', function (gateInfo) {
            var mappedGates = $.map(gateInfo, function (item) { return new gate(item)});
            self.alternateGates(mappedGates);
        });

        $('#movedialog').dialog({
            resizable: false,
            modal: true
        });
    };

    self.moveFlight = function(data){
        $('#movedialog').dialog('close');
        $.ajax({
            headers: {'Accept': 'application/json', 'Content-Type': 'application/json'},
            url: apiurl + 'api/gates/' + self.selectedGate().gatenumber + '/flights/' + self.selectedFlight.flightcode + '/gate/' + data.gatenumber,
            type: 'PUT',
            success: function (data) {
                self.loadFlights(self.selectedGate());
            },
            error: function(xhr, ajaxOptions,thrownError){
                var response = JSON.parse(xhr.responseText);
            }
        });
    };

    self.bumpFlights = function(data){
        $.ajax({
            headers: {'Accept': 'application/json', 'Content-Type': 'application/json'},
            url: apiurl + 'api/gates/' + self.selectedGate().gatenumber + '/flights/' + data.flightcode + '/bump',
            type: 'PUT',
            success: function (data) {
                self.loadFlights(self.selectedGate());
            }
        });
    };

    self.loadGates();
    self.loadFlights(new gate( {GateNumber: 23} ));
}
ko.applyBindings(new flightModel());