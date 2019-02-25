<template>
    <div>
        <b-col v-if="field.type ==='line'" :sm="field.gridCol" :offset-sm="field.gridOffset">
            <line-chart :data="viewModel[field.name]"></line-chart>
        </b-col>
        <b-col v-else-if="field.type ==='pie'" :sm="field.gridCol" :offset-sm="field.gridOffset">
            <pie-chart :data="[['Blueberry', 44], ['Strawberry', 23]]"></pie-chart>
        </b-col>
        <b-col v-else-if="field.type ==='column'" :sm="field.gridCol" :offset-sm="field.gridOffset">
            <column-chart :data="[['Sun', 32], ['Mon', 46], ['Tue', 28]]"></column-chart>
        </b-col>
        <b-col v-else-if="field.type ==='bar'" :sm="field.gridCol" :offset-sm="field.gridOffset">
            <bar-chart :data="[['X-Small', 5], ['Small', 27]]"></bar-chart>
        </b-col>
        <b-col v-else-if="field.type ==='area'" :sm="field.gridCol" :offset-sm="field.gridOffset">
            <bar-chart :data="viewModel[field.name]"></bar-chart>
        </b-col>
        <b-col v-else-if="field.type ==='highcharts'" :sm="field.gridCol" :offset-sm="field.gridOffset">
            <highcharts :options="viewModel[field.name]" ref="highcharts"></highcharts>
        </b-col>

        <b-col v-else-if="field.type ==='scatter'" :sm="field.gridCol" :offset-sm="field.gridOffset">
            <scatter-chart :data="[[174.0, 80.0], [176.5, 82.3]]" xtitle="Size" ytitle="Population"></scatter-chart>
        </b-col>
        <b-col v-else-if="field.type==='geo'" :sm="field.gridCol" :offset-sm="field.gridOffset">
            <geo-chart :data="[['United States', 44], ['Germany', 23], ['Brazil', 22]]"></geo-chart>
        </b-col>
        <b-col v-else-if="field.type==='timeline'" :sm="field.gridCol" :offset-sm="field.gridOffset">
            <timeline :data="[['Washington', '1789-04-29', '1797-03-03'], ['Adams', '1797-03-03', '1801-03-03']]"></timeline>
        </b-col>
    </div>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
        ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {


        },
 
    }

</script>
<style scoped>
</style>
