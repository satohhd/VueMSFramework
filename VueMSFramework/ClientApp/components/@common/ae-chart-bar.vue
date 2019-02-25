<template>
    <div class="value-kntiChart">
        <svg viewBox="0 0 240 15">
            <rect v-if="_workTime&&_startTime" fill="#EBCBB2" :x="_startTime" y="0" :width="_workTime" height="10" />
            <rect v-if="_restTime" fill="#FFF8F2" x="90" y="0" :width="_restTime" height="10" />
            <rect v-if="_overTime" fill="#D6AE8B" :x="_overTimeStart" y="0" :width="_overTime" height="10" />
            <rect v-if="_workTime2&&_startTime2" fill="#FFAD6E" fill-opacity="0.9" :x="_startTime2" y="7" :width="_workTime2" height="7" />
        </svg>
    </div>
</template>
<script>
    /* ******************************************************************
    *
    * ae-chart-bar
    *
    * in  string(hh:mm)    startTime
    * in  string(hh:mm)    endTime
    * in  string(hh:mm)    restTime
    * in  string(hh:mm)    overTime
    * in  string(hh:mm)    startTime2
    * in  string(hh:mm)    endTime2
    *
    ******************************************************************* */
    export default {
        props: ["startTime", "endTime", "restTime", "overTime", "startTime2", "endTime2"],
        data() {
            return {
                gap: 50,
            }
        },
        watch: {
        },
        methods: {
        },
        computed: {
            _startTime: function () {
                if (this.startTime) {
                    let val = (Number(this.startTime.split(':')[0]) + Number(this.startTime.split(':')[1]) / 60) * 10 - this.gap;
                    if (val <= 0) {
                        return 0.1;
                    } else {
                        return val;
                    }
                } else {
                    return 0;
                }
            },
            _endTime: function () {
                if (this.endTime) {
                    //let index = this.endTime.indexOf(":");
                    //return (Number(this.endTime.slice(0, index)) + Number(this.endTime.slice(index, index + this.endTime.length)) / 60) * 10 - this.gap;
                    return (Number(this.endTime.split(':')[0]) + Number(this.endTime.split(':')[1]) / 60) * 10 - this.gap;
                } else {
                    return 0;
                }
            },
            _workTime: function () {
                return this._endTime - this._startTime;
            },
            _restTime: function () {
                if (this.restTime) {

                    //let index = this.restTime.indexOf(":");
                    //return (Number(this.restTime.slice(0, index)) + Number(this.restTime.slice(index, index + this.restTime.length)) / 60) * 10;
                    return (Number(this.restTime.split(':')[0]) + Number(this.restTime.split(':')[1]) / 60) * 10;
               } else {
                    return 0;
                }
            },
            _overTime: function () {
                if (this.overTime){
                    //let index = this.overTime.indexOf(":");
                    //return (Number(this.overTime.slice(0, index)) + Number(this.overTime.slice(index, index + this.overTime.length)) / 60) * 10;
                    return (Number(this.overTime.split(':')[0]) + Number(this.overTime.split(':')[1]) / 60) * 10;
                } else {
                    return 0;
                }
            },
            _overTimeStart: function () {
                return this._endTime - this._overTime;
            },

            _startTime2: function () {
                if (this.startTime2) {
                    //let index = this.startTime2.indexOf(":");
                    //let val = (Number(this.startTime2.slice(0, index)) + Number(this.startTime2.slice(index, index + this.startTime2.length)) / 60) * 10 - this.gap;

                    let val = (Number(this.startTime2.split(':')[0]) + Number(this.startTime2.split(':')[1]) / 60) * 10 - this.gap;

                    if (val <= 0) {
                        return 0.1;
                    } else {
                        return val;
                    }
                } else {
                    return 0;
                }
            },
            _endTime2: function () {
                if (this.endTime2) {
                    //let index = this.endTime2.indexOf(":");
                    //return (Number(this.endTime2.slice(0, index)) + Number(this.endTime2.slice(index, index + this.endTime2.length)) / 60) * 10 - this.gap;

                    return (Number(this.endTime2.split(':')[0]) + Number(this.endTime2.split(':')[1]) / 60) * 10 - this.gap;

                } else {
                    return 0;
                }
            },
            _workTime2: function () {
                return this._endTime2 - this._startTime2;
            },


        },
        created: function () {
        },
        mounted: function () {
        },
        destroyed: function () {
        }

    }

</script>
<style scoped>
</style>

