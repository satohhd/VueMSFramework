<template>
    <ae-dynamic-page :page="page"
                     :startUp="startUp"
                     :viewModelClassName="viewModelClassName"
                     access="public"
                     @teller="listener" />
</template>

<script>
    import aeTransition from "../@common/ae-transition-utils.js"
    export default {
        mixins: [aeTransition],

        /** *****************************************************************
        *
        * data
        *
        ******************************************************************* */
        data() {
            return {
                page: "fukuri",
                startUp: { section: "calc", action: "calc" },
                viewModelClassName: "VueMSFramework.ViewModels.Fukuri.Index",
           }
        },


        methods: {

            /* ******************************************************************
             *
             * listener
             *
             * param  string           section
             * param  {}               field{event}
             * param  {}               viewModel{..}
             * return void
             *
             ******************************************************************* */
            listener: function (section, field, param) {
                switch (field.event.action) {
                    case "back":
                        this.$router.go(-1)
                        break;

                    default:
                        this.invoke(field.event, param, field.redirect);
                        break;
                }
            },
        },
    }
</script>
<style lang='scss'>

.kintai {
    .fukuri
    {
        button.btnSelect{
            background-color: #E9E9E9 !important;
            border-color: #E9E9E9 !important;
            color: black !important;
        }
           
        label.shainInfo{
            display:none;
        }
        label.yearMonth {
            display: none !important;
        }
        input.yearMonth {
            height: 35px;
        }
        .tabs {
            margin-top: 10px;
        }
        table.shainInfo thead {
            background-color: #E9E1DE;
        }
        table.kintaiList thead {
            background-color: #E9E1DE;
        }
        button.btn {
            background-color: #EE7708;
            color: #ffffff;
            border-color: #DB8D42;
        }
        .table-Sa {
            background-color: #EBF4F9;
            color: #3785AB;
        }
        .table-Su {
            background-color: #FAEEF2;
            color: #DE6990;
        }
        table th {
            padding: 0;
            margin: 0;
            text-align:center;
        }
        .value-kntiChart {
            width: 600px;
        }
        table.label-kntichart {
            border-collapse: collapse;
            table-layout: fixed;
            width: 600px;
            border: 1px dotted gray;
        }
        table.label-kntichart td {
            padding: 0;
            margin: 0;
            border: 1px dotted gray;
            border-collapse: collapse;
            text-align: center;
        }

        table.label-kntichart tr {
            padding: 0;
            margin: 0;
            border: 1px solid gray;
            border-collapse: collapse;
            width: 100%;
            text-align: center;
        }
        table.kintaiList tbody td:nth-child(9) {
            padding-left: 0;
            padding-right: 0;
            margin: 0;
        }
    }
}
@media screen and (max-width: 1024px) {
    .kintai {
        .search
        {
            table.kintaiList thead th:nth-child(9)
            {
                display: none;
            }
            table.kintaiList tbody td:nth-child(9) {
                display:none;
            }

        }
    }
}
</style>
<style scoped >
    .section {
        min-height: 450px;
    }

</style>
